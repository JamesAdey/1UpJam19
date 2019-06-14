using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    [SerializeField]
    GameObject playerPrefab;

    public List<GameObject> players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        manager = this;

        GameObject player = Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity);
        HumanBrain brain = player.GetComponent < HumanBrain >();
        brain.cam = Camera.main;

        players.Add(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
