using System.Collections;
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

    public void Awake()
    {
        blockingRadius = 4.5f;
    }

    private void Start()
    {
        NavMesh.singleton.StitchNodes(nodes);
        GameManager.manager.buildings.Add(gameObject);
    }




    void ShootAtTarget()
    {
        foreach(GameObject player in GameManager.manager.players)
        {
            Teams.Team enemyTeam = Teams.GetEnemyTeam(team);
            if (enemyTeam == Teams.Team.NONE || player.GetComponent<PlayerController>().team == enemyTeam)
            {
                if (InRange(player.transform, range))
                {
                    GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
                    bullet.GetComponent<BulletScript>().target = player.transform;
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
}
