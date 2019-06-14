using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMesh : MonoBehaviour
{
    List<NavNode> nodes;

    public static NavMesh singleton;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
    }

    private void OnDestroy()
    {
        if(singleton == this)
        {
            singleton = null;
        }
    }

    void StitchNodes(List<NavNode> newNodes)
    {
        foreach (NavNode oldNode in nodes)
        {
            foreach(NavNode newNode in newNodes)
            {
                // check for connection
                if (Physics.Linecast(oldNode.position, newNode.position))
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

    void UnstitchNodes(List<NavNode> nodesToRemove)
    {
        foreach (NavNode node in nodesToRemove)
        {
            node.DisconnectAll();
            nodes.Remove(node);
        }
    }
}
