using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public static BuildingInfo inf;

    [SerializeField]
    GameObject barracksPrefab;

    [SerializeField]
    GameObject towerPrefab;

    public enum BuildingType { BARRACKS, TOWER}

    // Start is called before the first frame update
    void Start()
    {
        inf = this;
    }

    public GameObject getPrefab(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.BARRACKS:
                return barracksPrefab;
            case BuildingType.TOWER:
                return towerPrefab;
            default:
                return null;
        }
    }
}
