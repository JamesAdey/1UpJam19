using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    [SerializeField]
    GameObject playerPrefab;

    public List<PlayerData> players = new List<PlayerData>();

    public PlayerData humanPlayer;

    private void Awake()
    {
        manager = this;
    }

    public PlayerData GetPlayer(Teams.Team t)
    {
        foreach(var player in players)
        {
            if(player.team == t)
            {
                return player;
            }
        }
        return null;
    }

    public static Teams.Team GetOpposingTeam(Teams.Team t)
    {
        if(t == Teams.Team.PLAYER)
        {
            return Teams.Team.AI;
        }
        if(t == Teams.Team.AI)
        {
            return Teams.Team.PLAYER;
        }
        return Teams.Team.NONE;
    }

    public PlayerData GetOpposingPlayer(Teams.Team t)
    {
        Teams.Team enemyTeam = GetOpposingTeam(t);
        return GetPlayer(enemyTeam);
    }

    // Start is called before the first frame update
    void Start()
    {

        humanPlayer = CreateHumanPlayer();
        players.Add(humanPlayer);

        PlayerData botPlayer = CreateBotPlayer();
        players.Add(botPlayer);
    }

    PlayerData CreateHumanPlayer()
    {
        PlayerData data = new PlayerData();
        GameObject playerObj = Instantiate(playerPrefab, new Vector3(0, 10, 10), Quaternion.identity);

        // Camera stuff
        HumanBrain brain = playerObj.AddComponent<HumanBrain>();
        brain.cam = Camera.main;
        Camera.main.GetComponent<TopDownCamera>().target = playerObj.transform;
        

        // Fill in the fields
        data.controller = playerObj.GetComponent<PlayerController>();
        data.team = Teams.Team.PLAYER;
        data.controller.team = Teams.Team.PLAYER;
        data.brain = brain;
        
        return data;
    }

    PlayerData CreateBotPlayer()
    {
        PlayerData data = new PlayerData();
        GameObject playerObj = Instantiate(playerPrefab, new Vector3(0, 10, -10), Quaternion.identity);
        
        // BRAAAAINS!
        BotBrain brain = playerObj.AddComponent<BotBrain>();

        // Fill in the fields
        data.controller = playerObj.GetComponent<PlayerController>();
        data.controller.team = Teams.Team.AI;
        data.team = Teams.Team.AI;
        data.brain = brain;

        return data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
