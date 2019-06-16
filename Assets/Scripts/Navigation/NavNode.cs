using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour
{

    [SerializeField]
    internal List<NavNode> neighbours = new List<NavNode>();
    private Transform thisTransform;

    public Vector3 Position { get { return thisTransform.position; } }
    
    // BEGIN NAV DATA
    public bool isClosed;
    public bool isUnseen;
    public float distToGoal;
    public NavNode parentNode;
    // END NAV DATA
    // Start is called before the first frame update
    void Awake()
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

    internal void ResetPathingData()
    {
        isClosed = false;
        isUnseen = true;
        distToGoal = float.MaxValue;
        parentNode = null;
    }

    public void Disconnect(NavNode other)
    {
        neighbours.Remove(other);
        other.neighbours.Remove(this);
    }

    public void DisconnectAll()
    {
        for(int i = neighbours.Count-1; i >= 0; i--)
        {
            NavNode neighbour = neighbours[i];
            neighbour.neighbours.Remove(this);
        }
        neighbours.Clear();
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
