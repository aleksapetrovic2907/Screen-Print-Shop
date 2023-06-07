using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Aezakmi.Tweens
{
    public abstract class TweenBase : MonoBehaviour
    {
        public Tweener Tweener { get; set; }
        public bool IsComplete { get; set; }

        [Header("Base Tween Settings")]
        [SerializeField] protected int loopCount;
        [SerializeField] protected float loopDuration;
        [SerializeField] protected LoopType loopType;
        [SerializeField] protected Ease loopEase;
        [SerializeField] protected float delay;
        [SerializeField] private bool playOnAwake;
        [SerializeField] private UnityEvent eventsOnPlayTween;
        [SerializeField] private UnityEvent eventsOnComplete;

        protected virtual void Awake()
        {
            if (playOnAwake) PlayTween();
        }

        public virtual void PlayTween()
        {
            SetTweener();
            Tweener.OnComplete(Complete);

            eventsOnPlayTween.Invoke();
            Tweener.Play();
        }
        public void PlayBackwards() => Tweener.PlayBackwards();
        public void Rewind() => Tweener.Rewind();
        public void AddDelegateOnComplete(UnityAction callback) => eventsOnComplete.AddListener(callback);

        protected abstract void SetTweener();
        protected virtual void Complete()
        {
            IsComplete = true;
            eventsOnComplete.Invoke();
        }
        private void OnDestroy() => Tweener.Kill();
    }
}
