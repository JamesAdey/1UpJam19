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

    Transform thisTransform;

    public override BuildingType GetBuildingType()
    {
        return BuildingType.TOWER;
    }

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
        UpdateVisuals();
    }




    void ShootAtTarget()
    {
        PlayerData enemyPlayer = GameManager.manager.GetOpposingPlayer(team);
        foreach (Minion minion in enemyPlayer.minions)
        {
            if (InRange(minion.Position, range))
            {
                FireAtTransform(minion.transform);
                return;
            }
        }

        if (InRange(enemyPlayer.controller.transform, range))
        {
            FireAtTransform(enemyPlayer.controller.transform);
            return;
        }

    }

    void FireAtTransform(Transform trans)
    {
        GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().target = trans;
        bullet.GetComponent<BulletScript>().team = team;
    }

    // Update is called once per frame
    public override void Update()
    {

        mostRecentShoot -= Time.deltaTime;

        if (mostRecentShoot < 0)
        {
            mostRecentShoot = shootTime;
            ShootAtTarget();
        }
        base.Update();
    }

    public override Vector3 GetPosition()
    {
        return thisTransform.position;
    }

    public override float GetAttackRange()
    {
        print("TOWER RANGE " + range);
        return range;
    }
}
