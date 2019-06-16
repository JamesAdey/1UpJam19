using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBrain : MonoBehaviour
{
    public abstract PlayerInput GetInputs();
    public BuildingType desiredBuildingType = BuildingType.TOWER;

    internal void SetDesiredBuilding(BuildingType b)
    {
        desiredBuildingType = b;
    }
}
