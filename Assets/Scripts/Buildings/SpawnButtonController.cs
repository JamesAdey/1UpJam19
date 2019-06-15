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
        SpawnGhost(towerPrefab, Teams.Team.PLAYER);
    }

    public void SpawnBarracks()
    {
        SpawnGhost(barracksPrefab, Teams.Team.PLAYER);
    }

    public void SpawnGhost(GameObject prefab, Teams.Team team)
    {
        GameObject ghost = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        PlayerInput input = GameManager.manager.humanPlayer.brain.GetInputs();
        ghost.GetComponent<Ghost>().input = input;
        ghost.GetComponent<BaseBuilding>().team = team;
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
