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

    [SerializeField]
    private Teams.Team myTeam = Teams.Team.NONE;
    [SerializeField]
    private float health = 10;
    [SerializeField]
    public float speed = 3;
    [SerializeField]
    private float attackRange = 3;
    [SerializeField]
    private DamageInfo damage;


    MovementCMap movementMap = new MovementCMap();
    CB_ChaseTower chaseTowerBehaviour = new CB_ChaseTower();
    
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
        myTeam = myBarracks.team;
    }

    public Vector3 Position => thisTransform.position;

    private void Attack()
    {
        RaycastHit hit;
        if(Physics.Raycast(thisTransform.position, thisTransform.forward, out hit, attackRange))
        {
            // check team
            var damageable = hit.transform.GetComponent<IDamageable>();
            if(damageable == null)
            {
                return;
            }
            if(damageable.GetTeam() != myTeam)
            {
                damageable.TakeDamage(damage);
            }
        }
    }

    // MINION
    // Move towards a tower
    // Attack nearby enemies


    public void TakeDamage(DamageInfo info)
    {
        health -= info.damage;
    }

    public Teams.Team GetTeam()
    {
        return myTeam;
    }
}
