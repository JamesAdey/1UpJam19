using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class LookPosCMap : ContextMap<float>
{
    private Vector3[] values;

    public override void Init(int size)
    {
        size = 1;
        slots = new float[size];
        values = new Vector3[size];
    }

    public void Decay()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] *= 0.5f;
        }
    }

    public void WriteLookPos(Vector3 value, float power)
    {
        int index = 0;
        if (slots[index] > power)
        {
            values[index] = value;
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

        return values[index];
    }

}
