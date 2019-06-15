using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : BaseBuilding
{

    Transform thisTransform;


    public override Vector3 GetPosition()
    {
        return thisTransform.position;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();

        PlayerData player = GameManager.manager.GetPlayer(team);

        player.buildings.Add(this);
        player.mainTower = this;

        NavMesh.singleton.StitchNodes(nodes);

        foreach(TeamMatChanger matChanger in GetComponentsInChildren<TeamMatChanger>())
        {
            matChanger.ChangeTeam(team);
        }
    }

   
    protected override void Die()
    {
        GameManager.manager.EndGame(GameManager.GetOpposingTeam(team));
    }
}
