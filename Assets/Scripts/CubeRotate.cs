using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    Transform thisTransform;

    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
       thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        thisTransform.Rotate(new Vector3(speed, speed, speed));
    }
}
