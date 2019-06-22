using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehav : BaseBehav
{
    public AttackBehav(PlayerInput inputs, Transform playerTrans, Teams.Team team) : base(inputs, playerTrans, team)
    {
    }

    private float mySqShootRange = 400;

    /// <summary>
    /// Returns the best building to shoot and its score.
    /// </summary>
    /// <param name="enemyPlayer"></param>
    /// <returns></returns>
    private (BaseBuilding, int) FindBestBuilding()
    {
        BaseBuilding bestBuilding = null;
        int bestScore = -1;

        int numBuilding = enemyPlayer.buildings.Count;

        foreach(BaseBuilding building in enemyPlayer.buildings)
        {
            float sqDist = (playerTrans.position - building.GetPosition()).sqrMagnitude;  

            //if it's in range, then we'll give it a score
            if ( sqDist < mySqShootRange )
            {
                float sqDistFromEnemyTower = (building.GetPosition() - enemyPlayer.mainTower.GetPosition()).sqrMagnitude;
                int myScore = 0;


                //check in attacking range
                if (sqDist < (building.GetAttackRange() * building.GetAttackRange()))
                {
                    myScore += 8;
                }

                //now other things are done based on its type
                switch(building.GetBuildingType())
                {
                    case BuildingType.MAIN:
                        myScore += 3;
                        break;
                    case BuildingType.BARRACKS:
                        myScore += 2;
                        break;
                    case BuildingType.TOWER:
                        myScore += 1;
                        break;
                }

                if(myScore > bestScore)
                {
                    bestBuilding = building;
                    bestScore = myScore;
                }
            }
        }

        return (bestBuilding, bestScore);
    }

    public int CheckEnemyPlayer()
    {
        int myScore = -1;

        if ((enemyPlayer.controller.transform.position - playerTrans.position).sqrMagnitude < mySqShootRange)
        {
            myScore += 4;

            if (enemyPlayer.controller.inputs.primaryAttack)
            {
                myScore += 4;
            }
        }

        return myScore;
    }

    public (Minion, int) FindBestMinion()
    {
        Minion bestMinion = null;
        int bestScore = -1;

        foreach(Minion minion in enemyPlayer.minions)
        {
            int myScore = 0;

            if((playerTrans.position - minion.Position).sqrMagnitude < 1)
            {
                myScore += 7;
            }
            else
            {
                myScore += 0;
            }

            if(myScore > bestScore)
            {
                bestMinion = minion;
                bestScore = myScore;
            }
        }

        return (bestMinion, bestScore);
    }


    private (Vector3, int) FindBest(List<(Vector3, int)> pairs)
    {
        (Vector3, int) currentBest = (Vector3.zero, -1);

        foreach((Vector3, int) pair in pairs)
        {
            if(pair.Item2 > currentBest.Item2){
                currentBest = pair;
            }
        }

        return currentBest;
    }

    private (Vector3, int) FindTargetPair((BaseBuilding, int) building, int playerScore, (Minion, int) bestMinion)
    {
        List<(Vector3, int)> pairs = new List<(Vector3, int)>();

        if (building.Item1 != null)
        {
            pairs.Add((building.Item1.GetPosition(), building.Item2));
        }

        if (playerScore > -1)
        {
            pairs.Add((enemyPlayer.controller.transform.position, playerScore));
        }

        if (bestMinion.Item1 != null)
        {
            pairs.Add((bestMinion.Item1.Position, bestMinion.Item2));
        }

        (Vector3, int) pair = FindBest(pairs);
        return pair;
    }



    public override void Think()
    {
        //find nearest building
        (BaseBuilding, int) building = FindBestBuilding();

        //check the status and distance of the player
        int playerScore = CheckEnemyPlayer();

        (Minion, int) bestMinion = FindBestMinion();
        (Vector3, int) pair = FindTargetPair(building, playerScore, bestMinion);

        if (pair.Item2 != -1)
        {
            inputs.lookPos = pair.Item1;
            inputs.primaryAttack = true;
        }
        else
        {
            inputs.primaryAttack = false;
        }

    }

}
