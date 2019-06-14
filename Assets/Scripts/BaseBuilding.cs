using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuilding : MonoBehaviour
{
    public float blockingRadius;
    public List<NavNode> nodes;
}
