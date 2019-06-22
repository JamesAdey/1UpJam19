using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehav
{


    public BaseBehav(PlayerInput inputs, Transform playerTrans, Teams.Team team)
    {
        this.inputs = inputs;
        this.myTransform = playerTrans;
        this.enemyPlayer = GameManager.manager.GetOpposingPlayer(team);
        this.myPlayer = GameManager.manager.GetPlayer(team);
        this.team = team;
    }

    protected PlayerData enemyPlayer;

    protected PlayerData myPlayer;

    protected Teams.Team team;

    protected PlayerInput inputs;

    protected Transform myTransform;

    public abstract void Think();
}
