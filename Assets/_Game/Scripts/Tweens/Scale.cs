using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public enum ScaleType
{ ScaleTo, ScaleBy }

namespace Aezakmi.Tweens
{
    public class Scale : TweenBase
    {
        [Header("Scale Tween Settings")]
        [SerializeField] private ScaleType scaleType;

        [ShowIf("m_isScaleTo")]
        [SerializeField] private Vector3 scale;
        [ShowIf("m_isScaleBy")]
        [SerializeField] private Vector3 amount;

        private Vector3 m_targetScale;
        private bool m_isScaleTo { get { return scaleType == ScaleType.ScaleTo; } }
        private bool m_isScaleBy { get { return scaleType == ScaleType.ScaleBy; } }

        protected override void SetTweener()
        {
            SetTargetScale();

            Tweener = transform
                .DOScale(m_targetScale, loopDuration)
                .SetLoops(loopCount, loopType)
                .SetEase(loopEase)
                .SetDelay(delay)
                .SetRelative(m_isScaleBy);
        }

        private void SetTargetScale() => m_targetScale = m_isScaleTo ? scale : amount;
    }
}