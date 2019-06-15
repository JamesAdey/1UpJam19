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


    private void Start()
    {
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
            GameObject nonGhost = Instantiate(nonGhostPrefab, transform.position, Quaternion.identity);
            nonGhost.GetComponent<BaseBuilding>().input = input;
            Destroy(gameObject);
        }
    }

    private bool CheckIfBlocked()
    {
        bool isBlocked = false;

        foreach (GameObject building in GameManager.manager.buildings)
        {
            isBlocked = InRange(building.transform, Mathf.Max(building.GetComponent<BaseBuilding>().blockingRadius, blockingRadius));
            if (isBlocked)
            {
                break;
            }

        }

        if (!isBlocked)
        {
            foreach (GameObject player in GameManager.manager.players)
            {
                isBlocked = InRange(player.transform, blockingRadius);
                if (isBlocked)
                {
                    break;
                }
            }
        }

        return isBlocked;
    }
}
