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
            float sqDist = (myTransform.position - building.GetPosition()).sqrMagnitude;  

            //if it's in range, then we'll give it a score
            if ( sqDist < mySqShootRange )
            {
                float sqDistFromEnemyTower = (building.GetPosition() - enemyPlayer.mainTower.GetPosition()).sqrMagnitude;
                int myScore = 0;


                //check in attacking range
                if (sqDist < (building.GetAttackRange() * building.GetAttackRange()))
                {
                    myScore += 15;
                }

                //now other things are done based on its type
                switch(building.GetBuildingType())
                {
                    case BuildingType.MAIN:
                        myScore += 4;
                        break;
                    case BuildingType.BARRACKS:
                        myScore += 3;
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

        if ((enemyPlayer.controller.transform.position - myTransform.position).sqrMagnitude < mySqShootRange)
        {
            myScore += 5;

            if (enemyPlayer.controller.inputs.primaryAttack)
            {
                myScore += 6;
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

            if((myTransform.position - minion.Position).sqrMagnitude < 4)
            {
                myScore += 8;
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

    public (Resource, int) FindBestResource()
    {
        Resource bestResource = null;
        int bestScore = -1;

        foreach(Resource resource in GameManager.manager.resources)
        {
            int myScore = 0;


            if(resource.capacity == 0)
            {
                continue;
            }

            if((resource.thisTransform.position - myTransform.position).sqrMagnitude < mySqShootRange)
            {
                if(myPlayer.resources < 50)
                {
                    myScore += 5;
                }
                else if(myPlayer.resources < 100)
                {
                    myScore += 2;
                }
             
                //is enemy is shooting at it already
                //in this case we want second highest priority so i'll just set
                //score to 13
                if((enemyPlayer.controller.inputs.lookPos - resource.thisTransform.position).sqrMagnitude < 25)
                {
                    myScore = 13;
                }

                if(myScore > bestScore)
                {
                    bestResource = resource;
                    bestScore = myScore;
                }

                
            }
        }

        return (bestResource, bestScore);
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

    private (Vector3, int) FindTarget((BaseBuilding, int) building, int playerScore, (Minion, int) bestMinion, (Resource, int) bestResource)
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

        if(bestResource.Item1 != null)
        {
            pairs.Add((bestResource.Item1.thisTransform.position, bestResource.Item2));
        }

        (Vector3, int) pair = FindBest(pairs);
        return pair;
    }



    public override void Think()
    {
        this.enemyPlayer = GameManager.manager.GetOpposingPlayer(team);
        this.myPlayer = GameManager.manager.GetPlayer(team);

        //find nearest building
        (BaseBuilding, int) bestBuilding = FindBestBuilding();

        //check the status and distance of the player
        int playerScore = CheckEnemyPlayer();

        (Minion, int) bestMinion = FindBestMinion();

        (Resource, int) bestResource = FindBestResource();

        (Vector3, int) pair = FindTarget(bestBuilding, playerScore, bestMinion, bestResource);

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
