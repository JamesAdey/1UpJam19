using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType { BARRACKS, TOWER, MAIN }

public class BuildingInfo : MonoBehaviour
{
    public static BuildingInfo inf;

    [SerializeField]
    GameObject barracksPrefab;

    [SerializeField]
    GameObject towerPrefab;

    [SerializeField]
    GameObject mainBuildingPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        inf = this;
    }

    public GameObject GetPrefab(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.BARRACKS:
                return barracksPrefab;
            case BuildingType.TOWER:
                return towerPrefab;
            case BuildingType.MAIN:
                return mainBuildingPrefab;
            default:
                return null;
        }
    }
}
