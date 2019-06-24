using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollower
{
    Transform myTransform;

    PlayerInput inputs;

    public bool onRoute;

    List<Vector3> currentPath = null;

    Vector3 nextNode = Vector3.zero;
    Vector3 prevNode = Vector3.zero;
    int goalIndex = 1;

    Vector3 currentDest = new Vector3(0, 4, 35);
    Vector3 currentStart = Vector3.zero;

    public RouteFollower(Transform myTransform, PlayerInput inputs)
    {
        this.myTransform = myTransform;
        this.inputs = inputs;
    }

    public void SetNewDestination(Vector3 dest)
    {
        currentDest = dest;
        List<Vector3> path = NavMesh.singleton.FindPath(myTransform.position, currentDest);

        if (path != null)
        {
            onRoute = true;
            currentPath = path;

            //we can assume these exist as every path has at least 2 nodes
            nextNode = path[1];
            prevNode = path[0];
            goalIndex = 1;

            currentStart = myTransform.position;

            moveToNextNode();
        }
    }

    private void moveToNextNode()
    {
        Vector3 directionVect = nextNode - prevNode;
        inputs.leftRightInput = directionVect.x;
        inputs.forwardBackwardInput = directionVect.z;
    }


    public void FollowPath()
    {
        if (!onRoute)
        {
            return;
        }

        float distReq = (nextNode - prevNode).sqrMagnitude;
        float disTraveled = (myTransform.position - prevNode).sqrMagnitude;

        if (disTraveled >= distReq)
        {
            if (currentPath.Count == goalIndex + 1)
            {
                onRoute = false;
                inputs.forwardBackwardInput = 0;
                inputs.leftRightInput = 0;
            }
            else
            {
                prevNode = nextNode;
                nextNode = currentPath[++goalIndex];
                moveToNextNode();
            }
        }

        onRoute = NavMesh.CheckReachable(prevNode, nextNode);
    }
}
