using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace Aezakmi.Transitions
{
    [Serializable]
    public class FadeScreenTransitionAnimation : TransitionAnimation
    {
        public UnityEvent onHalfway;

        [SerializeField] private Image image;
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;
        [SerializeField] private float duration;

        public override void StartTransition()
        {
            base.StartTransition();

            image.color = startColor;
            Sequence transition = DOTween.Sequence();
            Tween fadeIn = image.DOColor(endColor, duration).OnComplete(delegate { onHalfway.Invoke(); });
            Tween fadeOut = image.DOColor(startColor, duration).OnComplete(OnComplete);
            transition.Append(fadeIn).Append(fadeOut).Play();
        }
    }
}