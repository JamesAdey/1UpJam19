using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehav : BaseBehav
{
    public MoveBehav(PlayerInput inputs, Transform playerTrans, Teams.Team team) : base(inputs, playerTrans, team)
    {
    }

    public override void Think()
    {
        //change movement here
    }
}
