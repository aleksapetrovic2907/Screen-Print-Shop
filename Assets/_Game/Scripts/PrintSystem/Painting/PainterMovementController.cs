using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Aezakmi.PrintSystem.Painting
{
    public class PainterMovementController : MonoBehaviour
    {
        [Min(0)] public int paintCount;

        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private float paintDuration;
        [SerializeField] private float returnDuration;

        public void Paint(UnityAction onComplete)
        {
            Sequence paint = DOTween.Sequence();

            Tween moveDown = transform.DOMove(end.position, paintDuration).SetEase(Ease.InOutSine);
            Tween moveBack = transform.DOMove(start.position, returnDuration).SetEase(Ease.InOutSine);

            paint.Append(moveDown).Append(moveBack).SetLoops(paintCount).OnComplete(onComplete.Invoke).Play();
        }
    }
}