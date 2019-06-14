using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : BaseBuilding
{
    float timeSinceCheck = 0;
    float checkTime = 0.5f;

    bool canPlace;
    GhostRenderer[] ghostRenderers;


    private void Start()
    {
        ghostRenderers = GetComponentsInChildren<GhostRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        input = GameManager.manager.players[0].GetComponent<PlayerController>().inputs;
        SnapToMouse();

        timeSinceCheck += Time.deltaTime;

        if(timeSinceCheck > checkTime)
        {
            timeSinceCheck = 0;

            bool notinrange = true;

            foreach(GameObject building in GameManager.manager.buildings)
            {
                notinrange = !InRange(building.transform, Mathf.Max(building.GetComponent<BaseBuilding>().blockingRadius, blockingRadius));
                if (notinrange)
                {
                    break;
                }
                    
            }

            //check if we need to change textures
            if(notinrange != canPlace)
            {
                canPlace = notinrange;

                foreach(GhostRenderer render in ghostRenderers)
                {
                    render.SetMat(canPlace);
                }
            }

            
        }
    }
}
