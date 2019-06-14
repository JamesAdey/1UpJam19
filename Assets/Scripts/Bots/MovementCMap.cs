using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

class MovementCMap : ContextMap<float>
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
            float pow = 0;
            if (ang < 90)
            {
                pow = (90 - ang) / 90;    // scale to 1 at 0, and 0 at 90 degrees
                pow *= power;           // scale it by the power
                if (slots[i] < pow)
                {
                    slots[i] = pow;     // overwrite the slot if it's more power
                }
            }
        }
    }

    public Vector3 Evaluate()
    {
        int index = 0;
        float highest = slots[0];
        for (int i = 1; i < 8; i++)
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