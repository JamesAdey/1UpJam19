using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField]
    public int capacity;

    public int maxCap;

    public float timeSinceDeplete = 0;

    public float regenTime = 30;

    public GhostRenderer thisRenderer;

    public Transform thisTransform;

    private void Start()
    {
        maxCap = capacity;
        thisTransform = GetComponent<Transform>();
        thisRenderer = GetComponentInChildren<GhostRenderer>();
        GameManager.manager.resources.Add(this);
    }

    private void Update()
    {

        if(capacity == 0)
        {
            thisRenderer.SetMat(false);
        }
        else{
            thisRenderer.SetMat(true);
        }

        if(capacity == 0)
        {
            timeSinceDeplete += Time.deltaTime;

            if(timeSinceDeplete >= regenTime)
            {
                capacity = maxCap;
                timeSinceDeplete = 0;
            }
        }

    }

    public int depleteResource()
    {
        if(capacity != 0)
        {
            capacity -= 5;
            return 5;
        }

        return 0;
    
        
    }



    
}
