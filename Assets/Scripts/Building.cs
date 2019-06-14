using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public float blockingRadius;
    public List<NavNode> nodes;
}
