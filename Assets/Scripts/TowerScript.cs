using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;

    [SerializeField]
    public Transform shoot;


    [SerializeField]
    int range;

    int sqRange;
    
    float mostRecentShoot = 0;

    [SerializeField]
    float shootTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        sqRange = range * range;
    }


    bool inRange(Transform target)
    {
        Debug.Log("BANANA");
        Vector3 pos1 = target.position;
        Vector3 pos2 = transform.position;
        float sqDist = (pos1 - pos2).sqrMagnitude;
        return sqDist <= (range * range);
    }


    void ShootAtTarget()
    {
        foreach(GameObject player in GameManager.manager.players)
        {
            if (inRange(player.transform)) {
                GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().target = player.transform;
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
