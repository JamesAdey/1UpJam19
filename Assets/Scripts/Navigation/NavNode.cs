using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour
{

    [SerializeField]
    private List<NavNode> neighbours = new List<NavNode>();
    private Transform thisTransform;

    public Vector3 Position { get { return thisTransform.position; } }

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    public void Connect(NavNode other)
    {
        if (!neighbours.Contains(other))
        {
            neighbours.Add(other);
            other.Connect(this);
        }
    }

    public void Disconnect(NavNode other)
    {
        neighbours.Remove(other);
        other.neighbours.Remove(this);
    }

    public void DisconnectAll()
    {
        foreach (NavNode neighbour in neighbours)
        {
            Disconnect(neighbour);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.2f);
        foreach (NavNode node in neighbours)
        {
            if (node != null)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }

    public void VerifyNeighbours()
    {
        for (int i = neighbours.Count - 1; i >= 0; i--)
        {
            NavNode node = neighbours[i];
            if (node == null)
            {
                neighbours.RemoveAt(i);
            }
            else if(node == this)
            {
                neighbours.RemoveAt(i);
            }
        }

        foreach (NavNode node in neighbours)
        {
            node.Connect(this);
            this.Connect(node);
        }
    }
}
