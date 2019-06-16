using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class CB_WizardBuild : ContextBehaviour<float>
{

    int totalEnemyBuildings = 0;
    int totalMyBuildings = 0;

    public override void Process(ContextMap<float> map)
    {
        throw new System.NotImplementedException();
    }

    public void Process(MovementCMap movementMap, KeypressCMap keyboardMap, LookPosCMap lookMap, PlayerController controller)
    {
        PlayerData enemyPlayer = GameManager.manager.GetOpposingPlayer(controller.team);
        PlayerData myPlayer = GameManager.manager.GetPlayer(controller.team);

        // wizard build
        // MOVE near build spot

        totalEnemyBuildings = enemyPlayer.buildings.Count;
        totalMyBuildings = myPlayer.buildings.Count;

        int towerCount = 1;
        int barracksCount = 1;

        foreach(var building in enemyPlayer.buildings)
        {
            if(building is TowerBasic)
            {
                towerCount++;
            }
            else if(building is Barracks)
            {
                barracksCount++;
            }
        }

        foreach(var building in myPlayer.buildings)
        {
            if(building is TowerBasic)
            {
                towerCount--;
            }
            else if(building is Barracks)
            {
                barracksCount--;
            }
        }

        if(towerCount > 0 && myPlayer.resources > 25)
        {
            Vector3 buildPos = GetBuildPos(myPlayer, enemyPlayer);
            lookMap.WriteLookPos(buildPos, 3+towerCount);
            keyboardMap.WriteKey(BotKeys.BUILD_MODE, true, 1);
            keyboardMap.WriteKey(BotKeys.PRIMARY_ATTACK, true, 1);
            myPlayer.brain.SetDesiredBuilding(BuildingType.TOWER);
        }
        else if(barracksCount > 0 && myPlayer.resources > 50)
        {
            Vector3 buildPos = GetBuildPos(myPlayer, enemyPlayer);
            lookMap.WriteLookPos(buildPos,3+barracksCount);
            keyboardMap.WriteKey(BotKeys.BUILD_MODE, true, 1);
            keyboardMap.WriteKey(BotKeys.PRIMARY_ATTACK, true, 1);
            myPlayer.brain.SetDesiredBuilding(BuildingType.BARRACKS);
        }
        else
        {
            keyboardMap.WriteKey(BotKeys.BUILD_MODE, false, 1);
        }

        // Press keys to create buildings
    }

    Vector3 GetBuildPos(PlayerData myPlayer, PlayerData enemyPlayer)
    {

        int randomBuildingIndex = Random.Range(0, myPlayer.buildings.Count);

        BaseBuilding nearBuilding = myPlayer.buildings[randomBuildingIndex];

        float angle = Random.value * Mathf.PI * 2;
        Vector3 offset = new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle));
        offset *= nearBuilding.blockingRadius * 2;
        Vector3 buildPos = nearBuilding.GetPosition() + offset;
        buildPos.x = Mathf.Clamp(buildPos.x, -30, 30);
        buildPos.z = Mathf.Clamp(buildPos.z, -40, 40);
        return buildPos;
    }
}
