using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Aezakmi.Transitions;

namespace Aezakmi.PrintSystem
{
    public class PrintingMachineTurnManager : GloballyAccessibleBase<PrintingMachineTurnManager>
    {
        public Transform CurrentWing => wingAnimationDatas[currentTurn].wing;

        public int currentTurn = 0;

        [SerializeField] private Transform wingsParent;
        [SerializeField] private List<WingAnimationData> wingAnimationDatas;
        [SerializeField] private float wingMoveDuration;
        [SerializeField] private float wingsParentMoveDuration;
        [SerializeField] private FadeScreenTransitionAnimation goToPatternSelection;

        public void Setup()
        {
            currentTurn = 0;
            ResetRotations();
            ToggleWings();
            MoveToPatternSelection();
        }

        private void ToggleWings()
        {
            foreach (var wad in wingAnimationDatas)
                wad.wing.gameObject.SetActive(false);

            for(int i = 0; i < GameManager.Instance.patternsCount; i++)
                wingAnimationDatas[i].wing.gameObject.SetActive(true);
        }

        public void TurnFinished()
        {
            currentTurn++;

            if (currentTurn < GameManager.Instance.patternsCount)
            {
                AnimateWings();
                return;
            }

            StepsCountManager.Instance.MoveToNextStep();

            Sequence wingsMoveUp = DOTween.Sequence();
            foreach (var wad in wingAnimationDatas)
            {
                Tween wingMoveUp = wad.wing.DOLocalRotate(wad.upEuler, wingMoveDuration).SetEase(Ease.InOutSine);
                wingsMoveUp.Join(wingMoveUp);
            }

            wingsMoveUp.OnComplete(ReturnToIdle).Play();
        }

        public void AnimateWings()
        {
            Sequence fullAnimation = DOTween.Sequence();

            Sequence wingsMoveUp = DOTween.Sequence();
            foreach (var wad in wingAnimationDatas)
            {
                Tween wingMoveUp = wad.wing.DOLocalRotate(wad.upEuler, wingMoveDuration).SetEase(Ease.InOutSine);
                wingsMoveUp.Join(wingMoveUp);
            }

            Tween rotateWings = wingsParent.DOLocalRotate(Vector3.up * 90f, wingsParentMoveDuration).SetEase(Ease.InOutSine).SetRelative(true);

            Sequence wingsMoveDown = DOTween.Sequence();
            foreach (var wad in wingAnimationDatas)
            {
                Tween wingMoveDown = wad.wing.DOLocalRotate(wad.downEuler, wingMoveDuration).SetEase(Ease.InOutSine);
                wingsMoveDown.Join(wingMoveDown);
            }

            fullAnimation.Append(wingsMoveUp).Append(rotateWings).Append(wingsMoveDown).OnComplete(MoveToPatternSelection).Play();
        }

        private void MoveToPatternSelection()
        {
            StepsCountManager.Instance.ResetToSecondStep();
            goToPatternSelection.StartTransition();
        }

        private void ReturnToIdle()
        {
            // todo: give machine texture and type of shirt to print
            GameManager.Instance.GoToIdle();
        }

        private void ResetRotations()
        {
            wingsParent.localEulerAngles = Vector3.zero;

            foreach (var wad in wingAnimationDatas)
                wad.wing.transform.localEulerAngles = wad.downEuler;
        }
    }

    [Serializable]
    public class WingAnimationData
    {
        public Transform wing;
        public Vector3 upEuler;
        public Vector3 downEuler;
    }
}