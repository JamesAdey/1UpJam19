using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour
{

    [SerializeField]
    private List<NavNode> neighbours;
    private Transform thisTransform;

    public Vector3 position { get { return thisTransform.position; } }

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
        other.Disconnect(this);
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
        foreach (NavNode node in neighbours)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
