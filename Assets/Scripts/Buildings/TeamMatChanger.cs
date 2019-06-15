using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMatChanger : MonoBehaviour
{

    [SerializeField]
    private Material defaultMat;
    [SerializeField]
    private Material playerMat;
    [SerializeField]
    private Material aiMat;

    MeshRenderer myRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeTeam(Teams.Team team)
    {
        switch (team)
        {
            case Teams.Team.NONE:
                myRenderer.sharedMaterial = defaultMat;
                break;
            case Teams.Team.AI:
                myRenderer.sharedMaterial = aiMat;
                break;
            case Teams.Team.PLAYER:
                myRenderer.sharedMaterial = playerMat;
                break;
        }
    }
}
