using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContextBehaviourAI
{
    [System.Serializable]
    public abstract class ContextMap<T>
    {
        T[] slots;
        public abstract void Init(int size);

    }
}
