using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonController : MonoBehaviour
{

    public static SpawnButtonController spawner;

    [SerializeField]
    GameObject towerPrefab;

    [SerializeField]
    GameObject buildMenu;

    [SerializeField]
    GameObject barracksPrefab;

    public void Start()
    {
        spawner = this;
    }

    public void SpawnTowerBasic()
    {
        SpawnGhost(towerPrefab);
    }

    public void SpawnBarracks()
    {
        SpawnGhost(barracksPrefab);
    }

    public void SpawnGhost(GameObject prefab)
    {
        GameObject ghost = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        PlayerInput input = GameManager.manager.humanPlayer.brain.GetInputs();
        ghost.GetComponent<Ghost>().input = input;
        ghost.GetComponent<BaseBuilding>().team = Teams.Team.PLAYER;
        ghost.GetComponent<BaseBuilding>().SnapToMouse();
    }

    public void OpenBuild()
    {
        PlayerEnterMode(PlayerController.Mode.BUILD, Teams.Team.PLAYER);
        buildMenu.SetActive(true);
    }

    public void CloseBuild()
    {
        PlayerEnterMode(PlayerController.Mode.GAME, Teams.Team.PLAYER);
        buildMenu.SetActive(false);
    }

    public void PlayerEnterMode(PlayerController.Mode newMode, Teams.Team tagToChage)
    {

        Debug.Log("here");
        List<PlayerData> players = GameManager.manager.players;

        foreach(PlayerData player in players)
        {
            PlayerController con = player.controller;
            if(con.team == tagToChage)
            {
                con.myMode = newMode;
            }
        }
    }

    
}
