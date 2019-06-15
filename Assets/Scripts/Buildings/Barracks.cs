﻿using System;
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
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
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
        units.Add(m);
        nextSpawnTime = Time.time + spawnDelay;
    }

    internal void OnUnitKilled(Minion minion)
    {
        units.Remove(minion);
    }
}