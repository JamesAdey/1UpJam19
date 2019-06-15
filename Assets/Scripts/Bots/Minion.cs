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
    private float speed = 3;
    [SerializeField]
    private float attackDelay = 1;
    [SerializeField]
    private float attackRange = 3;
    public float AttackRange => attackRange;
    [SerializeField]
    private DamageInfo damage;


    MovementCMap movementMap = new MovementCMap();
    AttackCMap attackMap = new AttackCMap();
    CB_ChaseTower chaseTowerBehaviour = new CB_ChaseTower();
    CB_AttackEnemy attackEnemyBehaviour = new CB_AttackEnemy();
    
    public Vector3 desiredDir;
    public bool doAttack;
    private float nextAttackTime;

    private Barracks myBarracks;
    private PlayerData myPlayer;
    

    public void SetOwningPlayer(PlayerData player)
    {
        myPlayer = player;
        myTeam = player.team;
    }

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisTransform = GetComponent<Transform>();
        movementMap.Init(8);
        attackMap.Init(1);
        myPlayer.minions.Add(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementMap.Decay();
        attackMap.Decay();
        chaseTowerBehaviour.Process(movementMap, this);
        attackEnemyBehaviour.Process(attackMap, this);

        desiredDir = movementMap.Evaluate();
        Vector3 vel = desiredDir * speed;
        vel.y = thisRigidbody.velocity.y;
        thisRigidbody.velocity = vel;

        bool doAttack = attackMap.Evaluate();
        if (doAttack)
        {
            Attack();
        }

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
        if(myPlayer != null)
        {
            myPlayer.minions.Remove(this);
        }
        Destroy(gameObject);
    }

    internal void SetBarracks(Barracks barracks)
    {
        myBarracks = barracks;
        myTeam = myBarracks.team;
    }

    public Vector3 Position => thisTransform.position;

    private void Attack()
    {
        if(Time.time < nextAttackTime)
        {
            return;
        }
        nextAttackTime = Time.time + attackDelay;

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
