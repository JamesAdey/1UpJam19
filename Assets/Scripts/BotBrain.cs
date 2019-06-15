using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class BotBrain : BaseBrain
{
    [SerializeField]
    private PlayerInput inputs= new PlayerInput();

    MovementCMap movementMap = new MovementCMap();
    private Transform thisTransform;

    public override PlayerInput GetInputs()
    {
        return inputs;
    }

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        movementMap.Init(8);
        var prefab = BuildingInfo.inf.GetPrefab(BuildingType.BARRACKS);
        SpawnButtonController.spawner.SpawnBuilding(prefab, Teams.Team.AI, new Vector3(-20,3,-20) , Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        ThinkMovement();
        // TODO fill this in!
    }

    void ThinkMovement()
    {
        // update our inputs with movement behaviours

    }


}
