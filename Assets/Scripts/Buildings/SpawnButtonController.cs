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

    public void Start()
    {
        spawner = this;
    }

    public void SpawnGhost()
    {
        GameObject ghost = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
        PlayerInput input = GameManager.manager.players[0].GetComponent<PlayerController>().inputs;
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
        List<GameObject> players = GameManager.manager.players;

        foreach(GameObject player in players)
        {
            PlayerController con = player.GetComponent<PlayerController>();
            if(con.team == tagToChage)
            {
                con.myMode = newMode;
            }
        }
    }

    
}
