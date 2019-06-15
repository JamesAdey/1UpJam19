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
        PlayerData enemy = GameManager.manager.GetOpposingPlayer(m.GetTeam());
        
        foreach (BaseBuilding building in enemy.buildings)
        {
            highest = MarkMap(m.Position, building.GetPosition(), highest);
        }

        foreach (Minion enemyMinion in enemy.minions)
        {
            highest = MarkMap(m.Position, enemyMinion.Position,highest);
        }

        highest = MarkMap(m.Position, enemy.controller.transform.position, highest);

        for (int i = 0; i < weights.Count; i++)
        {
            float strength = 1.01f - (weights[i] / highest);
            contextMap.WriteDirection(directions[i], strength);
        }

    }

    private float MarkMap(Vector3 currentPos, Vector3 targetPos, float highest)
    {
        Vector3 dir = (targetPos - currentPos);
        float sqrMag = dir.sqrMagnitude;
        if (sqrMag > highest)
        {
            highest = sqrMag;
        }
        directions.Add(dir);
        weights.Add(sqrMag);
        return highest;
    }
}
