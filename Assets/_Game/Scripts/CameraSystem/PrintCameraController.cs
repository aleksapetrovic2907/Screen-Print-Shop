using UnityEngine;
using DG.Tweening;

namespace Aezakmi.CameraSystem
{
    public class PrintCameraController : MonoBehaviour
    {
        [SerializeField] private Repositioner teleportToStart;
        [SerializeField] private Repositioner teleportToPrint;
        [SerializeField] private Repositioner moveToColorSelection;
        [SerializeField] private Repositioner moveToPrintSlowly;

        public void MoveToStart() => Reposition(teleportToStart);
        public void MoveToPrint() => Reposition(teleportToPrint);
        public void MoveToColorSelection() => Reposition(moveToColorSelection);
        public void MoveToPrintSlowly() => Reposition(moveToPrintSlowly);

        private void Reposition(Repositioner repositioner)
        {
            Sequence sequence = DOTween.Sequence();
            Tween move = transform.DOMove(repositioner.position, repositioner.duration).SetEase(repositioner.ease);
            Tween rotate = transform.DORotate(repositioner.rotation, repositioner.duration).SetEase(repositioner.ease);
            sequence.Append(move).Join(rotate).Play();
        }
    }
}