﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBasic : BaseBuilding
{
    [SerializeField]
    public GameObject bulletPrefab;

    [SerializeField]
    public Transform shoot;


    [SerializeField]
    float range;
    
    float mostRecentShoot = 0;

    [SerializeField]
    float shootTime = 1;

    Transform thisTransform;

    public void Awake()
    {
        blockingRadius = 4.5f;
    }

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        NavMesh.singleton.StitchNodes(nodes);
        PlayerData player = GameManager.manager.GetPlayer(team);
        player.buildings.Add(this);
    }




    void ShootAtTarget()
    {
        foreach(PlayerData player in GameManager.manager.players)
        {
            Teams.Team enemyTeam = Teams.GetEnemyTeam(team);
            if (enemyTeam == Teams.Team.NONE || player.team == enemyTeam)
            {
                if (InRange(player.controller.transform, range))
                {
                    GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
                    bullet.GetComponent<BulletScript>().target = player.controller.transform;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        mostRecentShoot -= Time.deltaTime;

        if(mostRecentShoot < 0)
        {
            mostRecentShoot = shootTime;
            ShootAtTarget();
        }
    }

    public override Vector3 GetPosition()
    {
        return thisTransform.position;
    }
}
