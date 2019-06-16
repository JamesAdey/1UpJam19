using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMinion : Minion
{
    CB_ChaseTower chaseTowerBehaviour = new CB_ChaseTower();
    CB_AttackEnemy attackEnemyBehaviour = new CB_AttackEnemy();

    public override void MinionFixedUpdate()
    {
        chaseTowerBehaviour.Process(movementMap, this);
        attackEnemyBehaviour.Process(attackMap, this);
    }
}
