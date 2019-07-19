using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehav : BaseBehav
{
    Vector3 currentDest;

    int currentMoveScore = -1;

    RouteFollower routeFollower;

    public MoveBehav(PlayerInput inputs, Transform playerTrans, Teams.Team team) : base(inputs, playerTrans, team)
    {
        routeFollower = new RouteFollower(playerTrans, inputs);
    }

    public (Vector3,int) GetBuildingScore()
    {
        int dif = 0;

        //check the difference between my and my opponent's buildigs
        //if the enenmy player has more than us destroy it
        dif += (enemyPlayer.buildings.Count - myPlayer.buildings.Count);

        //check the max distance of any building to the tower
        //and to ours for that matter
        Vector3 bestTarget = Vector3.zero;
        int bestScore = -1;
        foreach(BaseBuilding building in enemyPlayer.buildings){

            //check if it's too close to me
            if((myTransform.position - building.GetPosition()).sqrMagnitude < (building.GetAttackRange() + 1) * (building.GetAttackRange() + 1))
            {
                if(bestScore < 100)
                {
                    bestScore = 100;
                    bestTarget = GameManager.manager.GetPlayer(team).mainTower.GetPosition();
                }
            }

            //check if it's in attacking range
            if((myTransform.position - building.GetPosition()).sqrMagnitude < 400)
            {
                int score = Mathf.Clamp(50 + 10 * dif, 0, 90);

                if(score > bestScore)
                {
                    bestScore = score;
                    bestTarget = myTransform.position;
                }
            }

            

        }

        return (bestTarget, bestScore);
    }

  


    public override void Think()
    {
        this.enemyPlayer = GameManager.manager.GetOpposingPlayer(team);
        this.myPlayer = GameManager.manager.GetPlayer(team);

        (Vector3, int) buildingScore = GetBuildingScore();

        if(buildingScore.Item2 > -1)
        {
            if(buildingScore.Item2 > currentMoveScore){

                currentMoveScore = buildingScore.Item2;
                routeFollower.SetNewDestination(buildingScore.Item1);
            }


        }
        else
        {
            currentMoveScore = -1;
            routeFollower.onRoute = false;
        }

        routeFollower.FollowPath();
    }
}
