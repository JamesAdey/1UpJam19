using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBehav : BaseBehav
{
    public BuildBehav(PlayerInput inputs, Transform playerTrans, Teams.Team team) : base(inputs, playerTrans, team)
    {
    }

    public override void Think()
    {
        //Decide whether or not to build and where
    }

    
}
