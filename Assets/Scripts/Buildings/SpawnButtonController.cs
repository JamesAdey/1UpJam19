using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButtonController : MonoBehaviour
{

    [SerializeField]
    GameObject towerPrefab;

    public void SpawnGhost()
    {
        GameObject ghost = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
        ghost.GetComponent<BaseBuilding>().SnapToMouse();
    }
}
