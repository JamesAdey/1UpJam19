﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BaseBrain brain;
    PlayerInput inputs;

    float speed = 5;

    Transform thisTransform;
    Rigidbody thisRigid;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        brain = GetComponent<BaseBrain>();
        thisRigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        inputs = brain.GetInputs();
        thisRigid.velocity = new Vector3(inputs.forwardBackwardInput * speed, thisRigid.velocity.y, -inputs.leftRightInput * speed);

        inputs.lookPos.y = thisTransform.position.y;
        thisTransform.LookAt(inputs.lookPos);
    }
}
