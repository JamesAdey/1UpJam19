using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCheck : MonoBehaviour
{
    [SerializeField]
    Teams.Team team;

    [SerializeField]
    int cost;

    PlayerData myPlayer;
    Button myButton;


    private void Start()
    {
        myPlayer = GameManager.manager.GetPlayer(team);
        myButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myPlayer.resources < cost)
        {
            myButton.interactable = false;
        }
        else
        {
            myButton.interactable = true;
        }
    }
}
