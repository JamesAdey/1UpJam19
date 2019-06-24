using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehav : BaseBehav
{
    Vector3 currentDest;

    RouteFollower routeFollower;

    public MoveBehav(PlayerInput inputs, Transform playerTrans, Teams.Team team) : base(inputs, playerTrans, team)
    {
        routeFollower = new RouteFollower(playerTrans, inputs);
    }

  


    public override void Think()
    {
    }
}
