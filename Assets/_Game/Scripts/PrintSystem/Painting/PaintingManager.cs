using UnityEngine;
using Aezakmi.Transitions;

namespace Aezakmi.PrintSystem.Painting
{
    public class PaintingManager : GloballyAccessibleBase<PaintingManager>
    {
        [SerializeField] private PainterMovementController painterMovementController;
        [SerializeField] private FadeScreenTransitionAnimation goToMachineTurning;
        
        public void StartPainting()
        {
            painterMovementController.gameObject.SetActive(true);
            painterMovementController.Paint(PaintingComplete);
        }

        private void PaintingComplete()
        {
            painterMovementController.gameObject.SetActive(false);
            goToMachineTurning.StartTransition();
        }
    }
}