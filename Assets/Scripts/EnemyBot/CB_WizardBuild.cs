using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;
using System;

public class CB_WizardBuild : ContextBehaviour<float>
{

    int totalEnemyBuildings = 0;
    int totalMyBuildings = 0;
    bool wantBuild = false;

    public override void Process(ContextMap<float> map)
    {
        throw new System.NotImplementedException();
    }

    void Process(MovementCMap movementMap, KeypressCMap keyboardMap, PlayerController controller)
    {
        PlayerData enemyPlayer = GameManager.manager.GetOpposingPlayer(controller.team);
        PlayerData myPlayer = GameManager.manager.GetPlayer(controller.team);

        // wizard build
        // MOVE near build spot

        totalEnemyBuildings = enemyPlayer.buildings.Count;
        totalMyBuildings = myPlayer.buildings.Count;
        
        if(totalEnemyBuildings >= totalMyBuildings)
        {
            wantBuild = true;
        }
        else
        {
            wantBuild = false;
        }

        // Press keys to create buildings
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
