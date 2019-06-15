using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRenderer : MonoBehaviour
{
    [SerializeField]
    Material goodMaterial;

    [SerializeField]
    Material badMaterial;

    MeshRenderer render;

    public void Awake()
    {
        render = GetComponent<MeshRenderer>();
    }

    public void SetMat(bool goodMat)
    {
        if (goodMat)
        {
            render.sharedMaterial = goodMaterial;
        }
        else
        {
            render.sharedMaterial = badMaterial;
        }
        
    }


}
