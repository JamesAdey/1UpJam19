using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;
using System;

public class Minion : MonoBehaviour, IDamageable
{
    Vector3 moveDir;
    Rigidbody thisRigidbody;
    Transform thisTransform;

    public float health = 10;

    MovementCMap movementMap = new MovementCMap();
    CB_ChaseTower chaseTowerBehaviour = new CB_ChaseTower();
    public float speed = 3;
    public float attackRange = 3;
    public Vector3 desiredDir;
    private Barracks myBarracks;
    

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
        movementMap.Decay();
        chaseTowerBehaviour.Process(movementMap, this);
        desiredDir = movementMap.Evaluate();
        Vector3 vel = desiredDir * speed;
        vel.y = thisRigidbody.velocity.y;
        thisRigidbody.velocity = vel;

        if (health < 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        if (myBarracks != null)
        {
            myBarracks.OnUnitKilled(this);
        }
    }

    internal void SetBarracks(Barracks barracks)
    {
        myBarracks = barracks;
    }

    public Vector3 Position => thisTransform.position;

    private void Attack()
    {
        RaycastHit hit;
        if(Physics.Raycast(thisTransform.position, thisTransform.forward, out hit, attackRange))
        {
            // check team
        }
    }

    // MINION
    // Move towards a tower
    // Attack nearby enemies


    public void TakeDamage(DamageInfo info)
    {
        health -= info.damage;
    }
}
