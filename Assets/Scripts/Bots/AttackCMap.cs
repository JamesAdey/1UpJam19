using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public class AttackCMap : ContextMap<float>
{
    bool[] values;
    public override void Init(int size)
    {
        slots = new float[1];
        values = new bool[1];
    }

    public void Write(bool attack, float power)
    {
        if(power > slots[0])
        {
            slots[0] = power;
            values[0] = attack;
        }
    }

    public void Decay()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] *= 0.5f;
        }
    }

    public bool Evaluate()
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

        return values[index];
    }
}