using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;
/// <summary>
/// A behaviour for moving towards the nearest tower
/// </summary>
public class CB_ChaseTower : ContextBehaviour<float>
{
    List<Vector3> directions = new List<Vector3>();
    List<float> weights = new List<float>();

    public override void Process(ContextMap<float> map)
    {
        throw new System.NotImplementedException();
    }

    public void Process(MovementCMap contextMap, Minion m)
    {
        directions.Clear();
        weights.Clear();
        float highest = 1;
        foreach(GameObject go in GameManager.manager.buildings)
        {
            Vector3 dir = (go.transform.position - m.Position);
            float sqrMag = dir.sqrMagnitude;
            if(sqrMag > highest)
            {
                highest = sqrMag;
            }
            directions.Add(dir);
            weights.Add(sqrMag);
        }

        for(int i = 0; i < weights.Count; i++)
        {
            float strength = 1 - (weights[i] / highest);
            contextMap.WriteDirection(directions[i], strength);
        }

    }
}
