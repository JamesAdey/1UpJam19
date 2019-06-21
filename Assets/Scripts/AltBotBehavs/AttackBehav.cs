using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehav : BaseBehav
{
    public AttackBehav(PlayerInput inputs, Transform playerTrans, Teams.Team team) : base(inputs, playerTrans, team)
    {
    }

    /// <summary>
    /// Returns the best building to shoot and its score.
    /// </summary>
    /// <param name="enemyPlayer"></param>
    /// <returns></returns>
    private (BaseBuilding, int) FindBestBuilding(PlayerData enemyPlayer)
    {
        BaseBuilding bestBuilding = null;
        int bestScore = -1;

        int numBuilding = enemyPlayer.buildings.Count;

        foreach(BaseBuilding building in enemyPlayer.buildings)
        {
            float sqDist = (playerTrans.position - building.GetPosition()).sqrMagnitude;  

            //if it's in range, then we'll give it a score
            if ( sqDist < 900 )
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
                        myScore += 2;
                        break;
                    case BuildingType.BARRACKS:
                        myScore += 1;
                        break;
                    case BuildingType.TOWER:
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

        if ((enemyPlayer.controller.transform.position - playerTrans.position).sqrMagnitude < 900)
        {
            myScore += 4;

            if (enemyPlayer.controller.inputs.primaryAttack)
            {
                myScore += 3;
            }
        }

        return myScore;

        
    }

    public override void Think()
    {

        //THIS SHOULD NOT BE HARD CODED
        PlayerData player = GameManager.manager.GetOpposingPlayer(Teams.Team.AI);

        //find nearest building
        (BaseBuilding, int) building = FindBestBuilding(player);

        //check the status and distance of the player
        int playerScore = CheckEnemyPlayer();

        
        //decides where to attack

        inputs.primaryAttack = true;

        if (playerScore > building.Item2)
        {
            inputs.lookPos = player.controller.transform.position;
        }
        else if(playerScore < building.Item2)
        {
            inputs.lookPos = building.Item1.GetPosition();
        }
        else
        {
            inputs.primaryAttack = false;
        }

        
    }
}
