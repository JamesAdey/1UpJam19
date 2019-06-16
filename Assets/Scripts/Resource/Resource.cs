using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField]
    public int capacity;



    public GhostRenderer thisRenderer;

    public Transform thisTransform;

    private void Start()
    {
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
    }

    public bool depleteResource()
    {
        if(capacity != 0)
        {
            capacity -= 5;
            return true;
        }

        return false;
        
    }



    
}
