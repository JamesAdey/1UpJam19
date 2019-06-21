using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    public GameObject AICanvas;

    [SerializeField]
    public GameObject playerCanvas;

    [SerializeField]
    public GameObject healthBarPrefab;

    public List<PlayerData> players = new List<PlayerData>();

    public List<Resource> resources = new List<Resource>();

    public PlayerData humanPlayer;

    public Text endText;

    public GameObject endPanel;

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

    public void AddResources(int numResources, Teams.Team team)
    {
        PlayerData player = GetPlayer(team);

        player.resources += numResources;
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
        SpawnButtonController.spawner.SpawnBuilding(BuildingType.MAIN, Teams.Team.PLAYER, new Vector3(0, 4, 35), Vector3.zero);

        PlayerData botPlayer = CreateBotPlayer();
        players.Add(botPlayer);
        SpawnButtonController.spawner.SpawnBuilding(BuildingType.MAIN, Teams.Team.AI, new Vector3(0, 4, -35), Vector3.zero);


    }

    PlayerData CreateHumanPlayer()
    {
        PlayerData data = new PlayerData();
        GameObject playerObj = Instantiate(playerPrefab, new Vector3(0, 10, 10), Quaternion.identity);

        // Camera stuff
        HumanBrain brain = playerObj.AddComponent<HumanBrain>();

        //BotBrain brain = playerObj.AddComponent<BotBrain>()
        brain.cam = Camera.main;
        Camera.main.GetComponent<TopDownCamera>().target = playerObj.transform;
        

        // Fill in the field
        data.controller = playerObj.GetComponent<PlayerController>();
        data.team = Teams.Team.PLAYER;
        data.controller.team = Teams.Team.PLAYER;
        data.brain = brain;

        //TEST
        data.resources = 100;

        GameObject healthBar = Instantiate(healthBarPrefab, playerCanvas.transform);

        data.controller.healthBar = healthBar;
        
 
        
        return data;
    }

    PlayerData CreateBotPlayer()
    {
        PlayerData data = new PlayerData();
        GameObject playerObj = Instantiate(playerPrefab, new Vector3(0, 10, -10), Quaternion.identity);

        // BRAAAAINS!
        //BotBrain brain = playerObj.AddComponent<BotBrain>();

        AltBotBrain brain = playerObj.AddComponent<AltBotBrain>();

        // Fill in the fields
        data.controller = playerObj.GetComponent<PlayerController>();
        data.controller.team = Teams.Team.AI;
        data.team = Teams.Team.AI;
        data.brain = brain;

        GameObject healthBar = Instantiate(healthBarPrefab, AICanvas.transform);

        data.controller.healthBar = healthBar;

        return data;
    }

    public void EndGame(Teams.Team winningTeam)
    {
        foreach(PlayerData player in players)
        {
            player.controller.myMode = PlayerController.Mode.END;
        }

        string winner = "NONE";

        switch (winningTeam)
        {
            case Teams.Team.AI:
                winner = "AI Wins!";
                break;
            case Teams.Team.PLAYER:
                winner = "You win!";
                break;
        }

        endPanel.SetActive(true);
        endText.text = winner;

    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
