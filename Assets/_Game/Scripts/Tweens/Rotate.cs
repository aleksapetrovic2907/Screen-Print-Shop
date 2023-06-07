using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public enum RotateType
{ RotateTo, RotateBy }

namespace Aezakmi.Tweens
{
    public class Rotate : TweenBase
    {
        [Header("Rotate Tween Settings")]
        [SerializeField] private RotateMode rotateMode;
        [SerializeField] private RotateType rotateType;

        [ShowIf("m_isRotateTo")]
        [SerializeField] private Vector3 rotation;
        [ShowIf("m_isRotateBy")]
        [SerializeField] private Vector3 amount;

        private Vector3 m_targetRotation;
        private bool m_isRotateTo { get { return rotateType == RotateType.RotateTo; } }
        private bool m_isRotateBy { get { return rotateType == RotateType.RotateBy; } }

        protected override void SetTweener()
        {
            SetTargetRotation();

            Tweener = transform
                .DOLocalRotate(m_targetRotation, loopDuration, rotateMode)
                .SetLoops(loopCount, loopType)
                .SetEase(loopEase)
                .SetDelay(delay);
        }

        private void SetTargetRotation() => m_targetRotation = m_isRotateTo ? rotation : amount;
    }
}