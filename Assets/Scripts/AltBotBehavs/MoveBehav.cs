using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehav : BaseBehav
{

    Vector3 currentDest = new Vector3(-30, 4, 30);

    bool onRoute = false;

    //path following stuff
    Vector3 nextNode = Vector3.zero;
    Vector3 prevNode = Vector3.zero;

    public MoveBehav(PlayerInput inputs, Transform playerTrans, Teams.Team team) : base(inputs, playerTrans, team)
    {

    }

    public override void Think()
    {
        if(!onRoute)
        {
            List<Vector3> path = NavMesh.singleton.FindPath(myTransform.position, currentDest);
            onRoute = true;

            if(path != null)
            {
                foreach (Vector3 node in path)
                {
                    Debug.Log(node.ToString());
                }
            }
            else
            {
                Debug.Log("ERROR: NO PATH FOUND");
            }

        }
    }
}
