using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;
using System;

public class MovementCMap : ContextMap<float>
{
    Vector3[] vectors;
    public override void Init(int size)
    {
        slots = new float[8];
        vectors = new Vector3[8];
        vectors[0] = new Vector3(0, 0, 1);
        vectors[1] = new Vector3(1, 0, 1);
        vectors[2] = new Vector3(1, 0, 0);
        vectors[3] = new Vector3(1, 0, -1);
        vectors[4] = new Vector3(0, 0, -1);
        vectors[5] = new Vector3(-1, 0, 1);
        vectors[6] = new Vector3(-1, 0, 0);
        vectors[7] = new Vector3(-1, 0, 1);
    }

    public void WriteDirection(Vector3 direction, float power)
    {
        for (int i = 0; i < 8; i++)
        {
            float ang = Vector3.Angle(direction, vectors[i]);
            if (ang < 90)
            {
                float pow = (90.0f - ang);    // scale to 1 at 0, and 0 at 90 degrees
                pow *= power/90;           // scale it by the power
                if (slots[i] < pow)
                {
                    slots[i] = pow;     // overwrite the slot if it's more power
                }
            }
        }
    }

    public void Decay()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] *= 0.5f;
        }
    }

    public Vector3 Evaluate()
    {
        int index = 0;
        float highest = slots[0];
        for (int i = 1; i < slots.Length; i++)
        {
            if (slots[i] > highest)
            {
                highest = slots[i];
                index = i;
            }
        }

        return vectors[index];
    }
}