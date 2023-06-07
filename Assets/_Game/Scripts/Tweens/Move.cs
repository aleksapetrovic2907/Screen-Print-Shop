using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public enum MoveType
{ MoveTo, MoveBy }

namespace Aezakmi.Tweens
{
    public class Move : TweenBase
    {
        [Header("Move Tween Settings")]
        [SerializeField] private MoveType moveType;

        [ShowIf("m_isMovingTo")]
        [SerializeField] private Vector3 position;
        [ShowIf("m_isMovingBy")]
        [SerializeField] private Vector3 amount;

        private Vector3 m_targetPosition;
        private bool m_isMovingTo { get { return moveType == MoveType.MoveTo; } }
        private bool m_isMovingBy { get { return moveType == MoveType.MoveBy; } }

        protected override void SetTweener()
        {
            SetTargetPosition();

            Tweener = transform
                .DOLocalMove(m_targetPosition, loopDuration)
                .SetLoops(loopCount, loopType)
                .SetEase(loopEase)
                .SetDelay(delay)
                .SetRelative(m_isMovingBy);
        }

        private void SetTargetPosition() => m_targetPosition = m_isMovingTo ? position : amount;
    }
}