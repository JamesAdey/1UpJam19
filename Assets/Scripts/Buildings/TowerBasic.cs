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

    private void Start()
    {
        navMesh.StitchNodes(nodes);
    }




    void ShootAtTarget()
    {
        foreach(GameObject player in GameManager.manager.players)
        {
            if (InRange(player.transform, range)) {
                GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().target = player.transform;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        input = GameManager.manager.players[0].GetComponent<PlayerController>().inputs;
        mostRecentShoot -= Time.deltaTime;

        if(mostRecentShoot < 0)
        {
            mostRecentShoot = shootTime;
            ShootAtTarget();
        }
    }
}
