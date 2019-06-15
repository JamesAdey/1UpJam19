﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Mode
    {
        GAME,
        BUILD
    }

    public Mode myMode = Mode.GAME;

    public BaseBrain brain;
    public PlayerInput inputs;
    public GameObject bulletPrefab;

    public float timeSinceShoot = 0;
    public float timeForShoot = 0.5f;

    float speed = 5;

    [SerializeField]
    public Teams.Team team;

    Transform thisTransform;
    Rigidbody thisRigid;

    TeamMatChanger[] matChangers;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        brain = GetComponent<BaseBrain>();
        thisRigid = GetComponent<Rigidbody>();
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if(matChangers == null)
        {
            matChangers = GetComponentsInChildren<TeamMatChanger>();
        }
        foreach (TeamMatChanger script in matChangers)
        {
            script.ChangeTeam(team);
        }
    }

    void Shoot()
    {
        Vector3 directionOfShoot = thisTransform.forward;
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (directionOfShoot), Quaternion.identity);
        Vector3 targetPoint = new Vector3(inputs.lookPos.x, thisTransform.position.y , inputs.lookPos.z);
        bullet.GetComponent<BulletScript>().targetPoint = targetPoint;
    }

    private void FixedUpdate()
    {
        switch (myMode)
        {
            case Mode.GAME:
                GameUpdate();
                break;
            case Mode.BUILD:
                BuildUpdate();
                break;
        }
    }

    private void BuildUpdate()
    {

    }

    private void GameUpdate()
    {
        inputs = brain.GetInputs();
        timeSinceShoot += Time.deltaTime;
        Vector3 moveVel = (inputs.strafeDir * inputs.leftRightInput) + (inputs.walkDir * inputs.forwardBackwardInput);
        moveVel.Normalize();
        moveVel *= speed;
        moveVel.y = thisRigid.velocity.y;
        thisRigid.velocity = moveVel;

        Vector3 flattenedLookPos = inputs.lookPos;
        flattenedLookPos.y = thisTransform.position.y;
        thisTransform.LookAt(flattenedLookPos);

        if (inputs.primaryAttack && timeSinceShoot > timeForShoot)
        {
            timeSinceShoot = 0;
            Shoot();
        }

        if (inputs.buildMode)
        {
            SpawnButtonController.spawner.OpenBuild();
            inputs.buildMode = false;
        }
    }
}
