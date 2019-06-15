using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuilding : MonoBehaviour
{

    [SerializeField]
    public float blockingRadius;

    public List<NavNode> nodes;
    public PlayerInput input;

    public Teams.Team team;

    public abstract Vector3 GetPosition();

    private TeamMatChanger[] matChangers;

    protected void UpdateVisuals()
    {
        if(matChangers == null)
        {

            matChangers = GetComponentsInChildren<TeamMatChanger>();
        }

        foreach (TeamMatChanger script in matChangers)
        {
            script.ChangeTeam(team);
        }
    }

    public void SnapToMouse()
    {
        Vector3 mousePos = input.lookPos;
        transform.position = mousePos;
        transform.eulerAngles = input.buildingEuler;
    }

    protected bool InRange(Transform target, float range)
    {
        Vector3 pos1 = target.position;
        Vector3 pos2 = transform.position;
        float sqDist = (pos1 - pos2).sqrMagnitude;
        return sqDist <= (range * range);
    }

    protected bool InRange(Vector3 targetPos, float range)
    {
        Vector3 pos = transform.position;
        float sqDist = (targetPos - pos).sqrMagnitude;
        return sqDist <= (range * range);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blockingRadius);
    }

}
