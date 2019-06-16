using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    public Transform target;

    [SerializeField]
    public Vector3 targetPoint;

    public Teams.Team team;

    public DamageInfo damageInf =  new DamageInfo();

    [SerializeField]
    public int damage = 10;

    void Awake()
    {
        damageInf.damage = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(target != null)
        {
            targetPoint = target.position;
            
        }
        transform.LookAt(targetPoint);
        GetComponent<Rigidbody>().velocity = transform.forward * 50;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        IDamageable obj = collision.gameObject.GetComponentInParent<IDamageable>();
        if(obj != null)
        {
            Debug.Log(collision.gameObject.name);
            damageInf.damage = damage;
            obj.TakeDamage(damageInf);
        }

        Resource res = collision.gameObject.GetComponentInParent<Resource>();
        if(res != null)
        {
            int resPoints = res.depleteResource();

            GameManager.manager.AddResources(resPoints, team);
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
