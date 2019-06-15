using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class CB_WizardAttack : ContextBehaviour<float>
{

    List<Vector3> moveDirections = new List<Vector3>();
    List<Vector3> lookDirections = new List<Vector3>();
    List<float> moveWeights = new List<float>();

    public override void Process(ContextMap<float> map)
    {
        throw new System.NotImplementedException();
    }

    void Process(MovementCMap movementMap, KeypressCMap keyboardMap, LookPosCMap lookMap, PlayerController controller)
    {
        PlayerData enemyPlayer = GameManager.manager.GetOpposingPlayer(controller.team);
        PlayerData myPlayer = GameManager.manager.GetPlayer(controller.team);

        Vector3 currentPos = controller.transform.position;
        // wizard move
        
        // do the movement
        ProcessMovement(movementMap, lookMap, enemyPlayer, currentPos);
        // Look at the thing we're moving towards
        // Press keys to do attacks
    }

    private void ProcessMovement(MovementCMap movementMap, LookPosCMap lookMap, PlayerData enemyPlayer, Vector3 currentPos)
    {
        moveDirections.Clear();
        moveWeights.Clear();
        lookDirections.Clear();
        float moveHighest = 1;

        float closeRange = 25;
        // move towards enemy minions
        foreach (var minion in enemyPlayer.minions)
        {
            float sqrDist = (minion.Position - currentPos).sqrMagnitude;
            // LOOK at the minion
            MarkLookMap(minion.Position);
            // possibly MOVE away from it
            if (sqrDist < closeRange)
            {
                MarkMovementMap(minion.Position, currentPos, moveHighest);
                
            }
            else
            {
                MarkMovementMap(currentPos, minion.Position, moveHighest);
            }
        }

        // move away from towers, but towards other buildings!
        foreach (var building in enemyPlayer.buildings)
        {
            // LOOK at the building
            MarkLookMap(building.GetPosition());
            // possibly MOVE away from it
            if (building is TowerBasic)
            {
                MarkMovementMap(building.GetPosition(), currentPos, moveHighest);
            }
            else
            {
                MarkMovementMap(currentPos, building.GetPosition(), moveHighest);
            }
        }


        for (int i = 0; i < moveWeights.Count; i++)
        {
            float strength = 1.01f - (moveWeights[i] / moveHighest);
            movementMap.WriteDirection(moveDirections[i], strength);
        }
    }

    private void MarkLookMap(Vector3 target)
    {
        lookDirections.Add(target);
    }

    private float MarkMovementMap(Vector3 from, Vector3 to, float highest)
    {
        Vector3 dir = (to - from);
        float sqrMag = dir.sqrMagnitude;
        if (sqrMag > highest)
        {
            highest = sqrMag;
        }
        moveDirections.Add(dir);
        moveWeights.Add(sqrMag);
        return highest;
    }

}
