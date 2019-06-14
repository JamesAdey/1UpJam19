using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    float speed = 5;
    Transform thisTransform;

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        thisTransform.position = Vector3.Lerp(thisTransform.position, desiredPos, speed * Time.deltaTime);
    }
}
