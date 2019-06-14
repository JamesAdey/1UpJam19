using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuilding : MonoBehaviour
{

    [SerializeField]
    public float blockingRadius;

    public List<NavNode> nodes;
    public PlayerInput input;

    [SerializeField]
    protected NavMesh navMesh; 

    protected void SnapToMouse()
    {
        Vector3 mousePos = input.lookPos;
        transform.position = mousePos;
    }

    protected bool InRange(Transform target, float range)
    {
        Vector3 pos1 = target.position;
        Vector3 pos2 = transform.position;
        float sqDist = (pos1 - pos2).sqrMagnitude;
        return sqDist <= (range * range);
    }

}
