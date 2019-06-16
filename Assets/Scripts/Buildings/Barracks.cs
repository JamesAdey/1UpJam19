using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : BaseBuilding
{
    public GameObject unitPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 10;

    public int maxUnits = 8;
    public List<Minion> units;

    private float nextSpawnTime;



    Transform thisTransform;
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        nextSpawnTime = Time.time+1;
        NavMesh.singleton.StitchNodes(nodes);
        PlayerData player = GameManager.manager.GetPlayer(team);
        player.buildings.Add(this);
        UpdateVisuals();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        bool spawnReady = Time.time > nextSpawnTime;
        bool hasFreeSlot = units.Count < maxUnits;
        
        if(spawnReady && hasFreeSlot)
        {
            SpawnUnit();
        }
        
    }

    private void SpawnUnit()
    {
        GameObject go = Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
        Minion m = go.GetComponent<Minion>();
        m.SetBarracks(this);
        PlayerData myPlayer = GameManager.manager.GetPlayer(team);
        m.SetOwningPlayer(myPlayer);
        units.Add(m);
        nextSpawnTime = Time.time + spawnDelay;
    }

    internal void OnUnitKilled(Minion minion)
    {
        units.Remove(minion);
    }

    protected override void Die()
    {
        foreach(var minion in units)
        {
            minion.ClearBarracks();
        }
        units.Clear();
        base.Die();
    }

    public override Vector3 GetPosition()
    {
        return thisTransform.position;
    }

    public override float GetAttackRange()
    {
        return 0;
    }
}
