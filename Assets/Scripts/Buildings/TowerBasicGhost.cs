using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBasicGhost : BaseBuilding
{
   

    // Update is called once per frame
    void Update()
    {
        input = GameManager.manager.players[0].GetComponent<PlayerController>().inputs;
        SnapToMouse();
    }
}
