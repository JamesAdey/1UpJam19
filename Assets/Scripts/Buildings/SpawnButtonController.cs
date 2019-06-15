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
        PlayerInput input = GameManager.manager.players[0].GetComponent<PlayerController>().inputs;
        ghost.GetComponent<Ghost>().input = input;
        ghost.GetComponent<BaseBuilding>().team = Teams.Team.PLAYER;
        ghost.GetComponent<BaseBuilding>().SnapToMouse();

    }
}
