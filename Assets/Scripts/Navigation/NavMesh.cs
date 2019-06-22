using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class NavMesh : MonoBehaviour
{
    List<NavNode> nodes = new List<NavNode>();

    [SerializeField]
    List<NavNode> defaultNodes = new List<NavNode>();

    public static NavMesh singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    private void Start()
    {
        StitchNodes(defaultNodes);
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }


    public List<Vector3> FindPath(Vector3 from, Vector3 to)
    {
        foreach (NavNode node in nodes)
        {
            node.ResetPathingData();
        }
        NavNode startNode = NearestNodeTo(from);
        NavNode endNode = NearestNodeTo(to);

        List<NavNode> openList = new List<NavNode>();
        openList.Add(startNode);
        startNode.isUnseen = false;

        while (!endNode.isUnseen)
        {

            NavNode current = openList[0];
            openList.RemoveAt(0);

            foreach (NavNode node in current.neighbours)
            {
                if (node.isUnseen)
                {
                    openList.Add(node);
                    node.isUnseen = false;
                    node.parentNode = current;
                }
            }
        }

        NavNode next = endNode;
        List<Vector3> path = new List<Vector3>();
        while (next != null)
        {
            path.Add(next.Position);
            next = next.parentNode;
        }
        return path;
    }

    private NavNode NearestNodeTo(Vector3 pos)
    {
        float shortest = float.MaxValue;
        NavNode closest = null;
        foreach (NavNode node in nodes)
        {
            float sqrDist = (pos - node.Position).sqrMagnitude;
            if (sqrDist < shortest)
            {
                if (Physics.Linecast(pos, node.Position))
                {
                    continue;   // skip if obscured
                }
                shortest = sqrDist;
                closest = node;
            }
        }
        return closest;
    }

    public void StitchNodes(List<NavNode> newNodes)
    {
        foreach (NavNode oldNode in nodes)
        {
            foreach (NavNode newNode in newNodes)
            {
                // check for connection
                if (!Physics.Linecast(oldNode.Position, newNode.Position))
                {
                    oldNode.Connect(newNode);
                }
            }

        }
        foreach (NavNode node in newNodes)
        {
            if (nodes.Contains(node))
            {
                continue;
            }
            nodes.Add(node);
        }
    }

    public void UnstitchNodes(List<NavNode> nodesToRemove)
    {
        foreach (NavNode node in nodesToRemove)
        {
            node.DisconnectAll();

        }
        foreach (NavNode node in nodesToRemove)
        {
            nodes.Remove(node);

        }
    }
#if UNITY_EDITOR

    [MenuItem("NavMesh/VerifyNeighbours")]
    public static void VerifyNeighbours()
    {
        NavNode[] allNodes = GameObject.FindObjectsOfType<NavNode>();
        foreach (NavNode node in allNodes)
        {
            node.VerifyNeighbours();
        }
    }
#endif
}
