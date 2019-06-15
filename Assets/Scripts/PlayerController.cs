﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        brain = GetComponent<BaseBrain>();
        thisRigid = GetComponent<Rigidbody>();
    }

    void Shoot()
    {
        Debug.Log("banana");
        Vector3 directionOfShoot = thisTransform.forward;
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (directionOfShoot), Quaternion.identity);
        Vector3 targetPoint = new Vector3(inputs.lookPos.x, thisTransform.position.y , inputs.lookPos.z);
        bullet.GetComponent<BulletScript>().targetPoint = targetPoint;
    }

    private void FixedUpdate()
    {
        inputs = brain.GetInputs();
        timeSinceShoot += Time.deltaTime;
        thisRigid.velocity = new Vector3(inputs.forwardBackwardInput * speed, thisRigid.velocity.y, -inputs.leftRightInput * speed);

        Vector3 flattenedLookPos = inputs.lookPos;
        flattenedLookPos.y = thisTransform.position.y;
        thisTransform.LookAt(flattenedLookPos);

        if (inputs.primaryAttack && timeSinceShoot > timeForShoot)
        {
            timeSinceShoot = 0;
            Shoot();
        }
    }
}
