using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    [SerializeField]
    GameObject playerPrefab;

    public List<GameObject> players = new List<GameObject>();

    public List<GameObject> buildings = new List<GameObject>();

    private void Awake()
    {
        manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        GameObject player = Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity);
        HumanBrain brain = player.GetComponent < HumanBrain >();
        brain.cam = Camera.main;
        Camera.main.GetComponent<TopDownCamera>().target = player.transform;

        players.Add(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
