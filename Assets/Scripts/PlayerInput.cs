using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInput
{
    public Vector3 walkDir;
    public Vector3 strafeDir;
    public float leftRightInput;
    public float forwardBackwardInput;
    public Vector3 lookPos;
    public bool primaryAttack;
    public bool buildMode;
    public Vector3 buildingEuler;
    
}
