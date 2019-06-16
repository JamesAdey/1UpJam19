using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;
using System;

public abstract class Minion : MonoBehaviour, IDamageable
{
    Vector3 moveDir;
    Rigidbody thisRigidbody;
    Transform thisTransform;

    [SerializeField]
    protected Teams.Team myTeam = Teams.Team.NONE;
    [SerializeField]
    protected float health = 10;
    [SerializeField]
    protected float speed = 3;
    [SerializeField]
    protected float attackDelay = 0.5f;
    [SerializeField]
    protected float attackRange = 3;
    public float AttackRange => attackRange;
    [SerializeField]
    protected DamageInfo damage;


    protected MovementCMap movementMap = new MovementCMap();
    protected AttackCMap attackMap = new AttackCMap();
    
    
    public Vector3 desiredDir;
    public bool doAttack;
    protected float nextAttackTime;

    protected Barracks myBarracks;
    protected PlayerData myPlayer;

    protected TeamMatChanger[] matChangers;

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
        UpdateVisuals();
        
    }

    protected void UpdateVisuals()
    {
        if (matChangers == null)
        {

            matChangers = GetComponentsInChildren<TeamMatChanger>();
        }

        foreach (TeamMatChanger script in matChangers)
        {
            script.ChangeTeam(myTeam);
        }
    }

    public abstract void MinionFixedUpdate();

    // Update is called once per frame
    void FixedUpdate()
    {
        MinionFixedUpdate();
        movementMap.Decay();
        attackMap.Decay();
        

        desiredDir = movementMap.Evaluate();
        Vector3 vel = desiredDir * speed;
        vel.y = thisRigidbody.velocity.y;
        thisRigidbody.velocity = vel;
        thisRigidbody.angularVelocity = Vector3.zero;

        desiredDir.y = 0;
        Quaternion desiredRot = Quaternion.LookRotation(desiredDir);
        thisRigidbody.rotation = Quaternion.RotateTowards(thisRigidbody.rotation, desiredRot,360*Time.deltaTime);

        doAttack = attackMap.Evaluate();
        if (doAttack)
        {
            Attack();
        }

        if (health <= 0)
        {
            OnDeath();
        }
    }

    protected void OnDeath()
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

    internal void ClearBarracks()
    {
        myBarracks = null;
    }

    public Vector3 Position => thisTransform.position;

    protected virtual void Attack()
    {
        if(Time.time < nextAttackTime)
        {
            return;
        }
        nextAttackTime = Time.time + attackDelay;

        Collider[] hits = Physics.OverlapSphere(thisTransform.position + (thisTransform.forward*1.1f), 0.5f);
        foreach(var hit in hits)
        {
            // check team
            var damageable = hit.transform.GetComponentInParent<IDamageable>();
            if (damageable == null)
            {
                continue;
            }
            if (damageable.GetTeam() != myTeam)
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
