using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBrain : BaseBrain
{
    [SerializeField]
    private PlayerInput inputs= new PlayerInput();

    public override PlayerInput GetInputs()
    {
        return inputs;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO fill this in!
    }
}
