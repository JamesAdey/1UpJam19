using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTextCon : MonoBehaviour
{

    Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        goldText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = GameManager.manager.GetPlayer(Teams.Team.PLAYER).resources.ToString();
    }
}
