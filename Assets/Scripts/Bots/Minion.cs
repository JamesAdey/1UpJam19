using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;



public class Minion : MonoBehaviour
{
    Vector3 moveDir;
    Rigidbody thisRigidbody;

    MovementCMap movementMap = new MovementCMap();

    public float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredDir = movementMap.Evaluate();
        thisRigidbody.velocity = desiredDir * speed;
    }

    // MINION
    // Move towards a tower

    void Think()
    {

    }
}
