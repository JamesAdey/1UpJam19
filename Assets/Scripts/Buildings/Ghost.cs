using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : BaseBuilding
{
    float timeSinceCheck = 0;
    float checkTime = 0.5f;

    bool first;

    public bool canPlace;
    GhostRenderer[] ghostRenderers;

    [SerializeField]
    GameObject nonGhostPrefab;

    [SerializeField]
    int resourceCost;

    Transform thisTransform;
    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        first = true;
        ghostRenderers = GetComponentsInChildren<GhostRenderer>();
        
    }


    // Update is called once per frame
    void Update()
    {
        SnapToMouse();


        timeSinceCheck += Time.deltaTime;

        if(timeSinceCheck > checkTime)
        {
            timeSinceCheck = 0;

            canPlace = !CheckIfBlocked();


            first = false;


            foreach (GhostRenderer render in ghostRenderers)
            {
                render.SetMat(canPlace);
            }
            


        }

        if (input.primaryAttack && canPlace)
        {
            GameManager.manager.GetPlayer(team).resources -= resourceCost;
            GameObject nonGhost = Instantiate(nonGhostPrefab, transform.position, transform.rotation);
            nonGhost.GetComponent<BaseBuilding>().input = input;
            nonGhost.GetComponent<BaseBuilding>().team = team;
            Destroy(gameObject);
        }

        if (input.escape)
        {
            Destroy(gameObject);
        }
    }

    private bool CheckIfBlocked()
    {
        foreach(PlayerData player in GameManager.manager.players)
        {
            // CHECK BUILDINGS
            foreach (BaseBuilding building in player.buildings)
            {
                if(InRange(building.transform, Mathf.Max(building.blockingRadius, blockingRadius)))
                {
                    return true;
                }

            }

            // CHECK MINIONS
            foreach (Minion minion in player.minions)
            {
                if(InRange(minion.Position, blockingRadius))
                {
                    return true;
                }
            }

            // CHECK PLAYER OBJECTS
            if (InRange(player.controller.transform.position, blockingRadius))
            {
                return true;
            }

            // CHECK RESOURCES ROCKS
            foreach(Resource resource in GameManager.manager.resources)
            {
                if((InRange(resource.thisTransform.position, blockingRadius)))
                {
                    return true;
                }
            }

            //CHECK FUNDS
            if (GameManager.manager.GetPlayer(team).resources < resourceCost)
            {
                return true;
            }
        }
        return false;
    }

    public override Vector3 GetPosition()
    {
        return thisTransform.position;
    }
}
