using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class CB_AttackEnemy : ContextBehaviour<bool>
{
    public override void Process(ContextMap<bool> map)
    {
        throw new System.NotImplementedException();
    }

    public void Process(AttackCMap contextMap, Minion m)
    {

        PlayerData enemy = GameManager.manager.GetOpposingPlayer(m.GetTeam());
        float sqrRange = m.AttackRange * m.AttackRange;

        // CHECK ENEMY
        Vector3 diff = enemy.controller.transform.position - m.Position;
        float sqrMag = diff.sqrMagnitude;
        if (sqrMag < sqrRange)
        {
            contextMap.Write(true, 1);
            return;
        }

        // CHECK BUILDING
        foreach (BaseBuilding building in enemy.buildings)
        {
            diff = building.GetPosition() - m.Position;
            float buildingSqrRange = building.blockingRadius * building.blockingRadius;
            sqrMag = diff.sqrMagnitude;
            if (sqrMag < buildingSqrRange)
            {
                contextMap.Write(true, 1);
                return;
            }
        }

        // CHECK MINIONS
        foreach (Minion enemyMinion in enemy.minions)
        {
            diff = enemyMinion.Position - m.Position;
            sqrMag = diff.sqrMagnitude;
            if (sqrMag < sqrRange)
            {
                contextMap.Write(true, 1);
                return;
            }
        }

        contextMap.Write(false, 1);
    }
}
