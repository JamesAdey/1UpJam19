using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class CB_WizardResources : ContextBehaviour<float>
{

    List<Vector3> moveDirections = new List<Vector3>();
    List<Vector3> lookDirections = new List<Vector3>();
    List<float> moveWeights = new List<float>();

    public override void Process(ContextMap<float> map)
    {
        throw new System.NotImplementedException();
    }

    public void Process(MovementCMap movementMap, KeypressCMap keyboardMap, LookPosCMap lookMap, PlayerController controller)
    {
        PlayerData enemyPlayer = GameManager.manager.GetOpposingPlayer(controller.team);
        PlayerData myPlayer = GameManager.manager.GetPlayer(controller.team);

        Vector3 currentPos = controller.transform.position;
        // wizard move

        float desire = 3;
        if (myPlayer.resources > 0)
        {
            desire = (enemyPlayer.resources + 10) / myPlayer.resources;
        }


        // do the movement
        ProcessMovement(movementMap, lookMap, keyboardMap, desire, currentPos);
        // Look at the thing we're moving towards
        // Press keys to do attacks
    }

    private void ProcessMovement(MovementCMap movementMap, LookPosCMap lookMap, KeypressCMap keyboardMap, float desire, Vector3 currentPos)
    {
        moveDirections.Clear();
        moveWeights.Clear();
        lookDirections.Clear();
        float moveHighest = 1;



        foreach (var res in GameManager.manager.resources)
        {
            Vector3 resourcePos = res.thisTransform.position;
            MarkLookMap(resourcePos);
            moveHighest = MarkMovementMap(currentPos, resourcePos, moveHighest);
        }


        for (int i = 0; i < moveWeights.Count; i++)
        {
            float strength = 1.01f - (moveWeights[i] / moveHighest);
            strength *= desire;
            // write in
            movementMap.WriteDirection(moveDirections[i], strength);
            lookMap.WriteLookPos(lookDirections[i], strength);
            keyboardMap.WriteKey(BotKeys.PRIMARY_ATTACK, true, strength);
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
