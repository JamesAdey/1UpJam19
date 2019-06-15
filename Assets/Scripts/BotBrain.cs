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
