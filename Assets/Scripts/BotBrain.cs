using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class BotBrain : BaseBrain
{
    [SerializeField]
    private PlayerInput inputs= new PlayerInput();

    MovementCMap movementMap = new MovementCMap();
    KeypressCMap keyboardMap = new KeypressCMap();
    LookPosCMap lookMap = new LookPosCMap();
    CB_WizardAttack attackBehaviour = new CB_WizardAttack();
    CB_WizardResources resourceBehaviour = new CB_WizardResources();
    CB_WizardBuild buildBehaviour = new CB_WizardBuild();

    private Transform thisTransform;
    public Vector3 moveDir;
    public override PlayerInput GetInputs()
    {
        return inputs;
    }

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        movementMap.Init(8);
        keyboardMap.Init(2);
        lookMap.Init(1);

        var prefab = BuildingInfo.inf.GetPrefab(BuildingType.BARRACKS);
        SpawnButtonController.spawner.SpawnBuilding(prefab, Teams.Team.AI, new Vector3(-20,3,-20) , Vector3.zero);

    }

    // Update is called once per frame
    void Update()
    {
        Think();
        // TODO fill this in!
    }

    void Think()
    {
        movementMap.Decay();
        keyboardMap.Decay();
        lookMap.Decay();

        inputs.walkDir = Vector3.forward;
        inputs.strafeDir = Vector3.right;

        PlayerData player = GameManager.manager.GetPlayer(Teams.Team.AI);
        attackBehaviour.Process(movementMap, keyboardMap, lookMap, player.controller);
        resourceBehaviour.Process(movementMap, keyboardMap, lookMap, player.controller);
        buildBehaviour.Process(movementMap, keyboardMap, lookMap, player.controller);

        // evaluate keys
        inputs.primaryAttack = keyboardMap.GetKeyPress(BotKeys.PRIMARY_ATTACK);
        inputs.buildMode = keyboardMap.GetKeyPress(BotKeys.BUILD_MODE);

        // evaluate movement
        moveDir = movementMap.Evaluate();
        inputs.forwardBackwardInput = moveDir.z;
        inputs.leftRightInput = moveDir.x;

        // evaluate look pos
        inputs.lookPos = lookMap.Evaluate();
        // update our inputs with movement behaviours

        PlayerData enemyPlayer = GameManager.manager.GetOpposingPlayer(Teams.Team.AI);
        Vector3 buildAngles = GetBuildingEulers(inputs.lookPos, enemyPlayer);
        inputs.buildingEuler = buildAngles;
        inputs.desiredBuilding = desiredBuildingType;

    }

    

    Vector3 GetBuildingEulers(Vector3 buildPos, PlayerData enemyPlayer)
    {
        Vector3 endPos = Vector3.zero;
        foreach (BaseBuilding b in enemyPlayer.buildings)
        {
            if (b is MainTower)
            {
                endPos = b.GetPosition();
                break;
            }
        }
        endPos.y = buildPos.y;
        Vector3 dir = endPos - buildPos;
        return Quaternion.LookRotation(dir, Vector3.up).eulerAngles;
    }


}
