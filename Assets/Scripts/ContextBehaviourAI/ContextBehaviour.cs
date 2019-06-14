using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContextBehaviourAI
{
    public abstract class ContextBehaviour<T>
    {
        public abstract void Process(ContextMap<T> map);
    }
}