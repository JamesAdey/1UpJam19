using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<Minion> minions = new List<Minion>();
    public List<BaseBuilding> buildings = new List<BaseBuilding>();
    public PlayerController controller;
    public BaseBrain brain;
    public Teams.Team team;
    public Material material;
}
