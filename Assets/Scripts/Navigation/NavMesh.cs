using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class NavMesh : MonoBehaviour
{
    List<NavNode> nodes = new List<NavNode>();

    public static NavMesh singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
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
            nodes.Remove(node);
        }
    }
#if UNITY_EDITOR

    [MenuItem("NavMesh/VerifyNeighbours")]
    public static void VerifyNeighbours()
    {
        NavNode[] allNodes = GameObject.FindObjectsOfType<NavNode>();
        foreach(NavNode node in allNodes)
        {
            node.VerifyNeighbours();
        }
    }
#endif
}
