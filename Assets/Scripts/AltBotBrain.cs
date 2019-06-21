using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltBotBrain : BaseBrain 
{

    PlayerInput inputs;

    List<BaseBehav> behaviours = new List<BaseBehav>();

    float timeBetweenThink = 0.5f;
    float timeSinceThink = 0.5f;

    public override PlayerInput GetInputs()
    {
        return inputs;
    }

    private void Awake()
    {
        inputs = new PlayerInput();
        behaviours.Add(new AttackBehav(inputs, transform, Teams.Team.AI));
        behaviours.Add(new MoveBehav(inputs, transform, Teams.Team.AI));
        behaviours.Add(new BuildBehav(inputs, transform, Teams.Team.AI));
        

        inputs.walkDir = Vector3.forward;
        inputs.strafeDir = Vector3.right;
    }

    // Start is called before the first frame update
    void Start()
    {

        inputs.primaryAttack = false;

        inputs.buildMode = false;

        inputs.escape = false;

        inputs.lookPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceThink += Time.deltaTime;

        if(timeSinceThink >= timeBetweenThink)
        {
            timeSinceThink = 0;
            foreach(BaseBehav behav in behaviours)
            {
                behav.Think();
            }
        }
    
        

    }
}
