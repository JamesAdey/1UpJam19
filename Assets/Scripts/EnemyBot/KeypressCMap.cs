using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextBehaviourAI;

public enum BotKeys
{
    PRIMARY_ATTACK,
    BUILD_MODE
}

public class KeypressCMap : ContextMap<float>
{
    bool[] values;
    public override void Init(int size)
    {
        size = 2;
        slots = new float[size];
        values = new bool[size];
    }

    public void Decay()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] *= 0.5f;
        }
    }

    public void WriteKey(BotKeys key, bool value, float power)
    {
        int index = (int)key;
        if(slots[index] < power)
        {
            slots[index] = power;
            values[index] = value;
        }
    }

    public bool Evaluate()
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

    public bool GetKeyPress(BotKeys key)
    {
        return values[(int)key];
    }
}