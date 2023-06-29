using System;
using UnityEngine.Events;

namespace Aezakmi.Transitions
{
    [Serializable]
    public class TransitionAnimation
    {
        public UnityEvent onStart;
        public UnityEvent onComplete;

        public virtual void StartTransition()
        {
            if(onStart != null)
                onStart.Invoke();
        }

        public virtual void OnComplete()
        {
            if(onComplete != null)
                onComplete.Invoke();
        }
    }
}