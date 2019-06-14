using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target.position);
        GetComponent<Rigidbody>().velocity = transform.forward * 50;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
