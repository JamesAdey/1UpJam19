using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams
{
    public enum Team{ AI, PLAYER, NONE};

    public static Team GetEnemyTeam(Team team)
    {
        switch (team)
        {
            case Team.AI:
                return Team.PLAYER;
            case Team.PLAYER:
                return Team.AI;
            default:
                return Team.NONE;
        }


    }
}
