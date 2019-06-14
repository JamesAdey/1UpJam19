using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContextBehaviourAI
{
    [System.Serializable]
    public abstract class ContextMap<T>
    {
        protected T[] slots;
        public abstract void Init(int size);
    }
}
