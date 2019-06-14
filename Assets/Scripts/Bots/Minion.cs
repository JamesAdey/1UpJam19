using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class Minion : MonoBehaviour
{
    Vector3 moveDir;
    Rigidbody thisRigidbody;
    Transform thisTransform;

    MovementCMap movementMap = new MovementCMap();
    CB_ChaseTower chaseTowerBehaviour = new CB_ChaseTower();
    public float speed = 3;
    public Vector3 desiredDir;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisTransform = GetComponent<Transform>();
        movementMap.Init(8);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        chaseTowerBehaviour.Process(movementMap, this);
        desiredDir = movementMap.Evaluate();
        Vector3 vel = desiredDir * speed;
        vel.y = thisRigidbody.velocity.y;
        thisRigidbody.velocity = vel;
    }

    public Vector3 Position => thisTransform.position;

    // MINION
    // Move towards a tower

    void Think()
    {

    }
}
