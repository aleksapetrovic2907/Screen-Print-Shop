using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Aezakmi.StoreSystem.Printers;

namespace Aezakmi.StoreSystem.StackSystem
{
    public class Carrier : MonoBehaviour
    {
        public int CurrentCarriage => stack.Count;
        public bool IsEmpty => CurrentCarriage == 0;
        public bool IsFull => CurrentCarriage == maxCapacity;

        public int maxCapacity;
        public List<StackableShirt> stack = new List<StackableShirt>();
        public Transform shirtsParent;

        #region ANIMATION
        [Header("Animation Settings")]
        [SerializeField] private float heightMargin;
        [SerializeField] private float animationDuration;
        [SerializeField] private Vector3 eulerInStack;
        [SerializeField] private Ease rotateEase;
        [SerializeField] private Ease jumpToStackEase;
        #endregion

        public void AddShirtToStack(StackableShirt stackableShirt)
        {
            if (IsFull) return;

            stack.Add(stackableShirt);
            stackableShirt.GetComponent<Collider>().enabled = false;
            stackableShirt.transform.parent = shirtsParent;
            AnimateBlockToStack(stackableShirt);
            UpdateBlockPositions();
        }

        private void AnimateBlockToStack(StackableShirt shirt)
        {
            shirt.animated = true;

            var position = GetPositionInStack(CurrentCarriage - 1);
            Tween moveToStack = shirt.transform.DOLocalJump(position, 0, 1, animationDuration).SetEase(jumpToStackEase);
            Tween rotate = shirt.transform.DOLocalRotate(eulerInStack, animationDuration).SetEase(rotateEase);

            Sequence shirtToStack = DOTween.Sequence();
            shirtToStack.Append(moveToStack).Join(rotate).OnComplete(delegate { shirt.animated = false; }).Play();
        }

        private void UpdateBlockPositions()
        {
            for (int i = 0; i < CurrentCarriage; i++)
            {
                // DOTween.Kill(stack[i]);
                stack[i].transform.DOLocalMove(GetPositionInStack(i), animationDuration).SetEase(rotateEase).Play();
            }
        }

        private Vector3 GetPositionInStack(int index)
        {
            Vector3 lastPosition = Vector3.zero;

            for (int i = 0; i < index; i++)
            {
                lastPosition += (stack[i].size.y + heightMargin) * Vector3.up;
            }

            return lastPosition;
        }

        public void AddShirtToShelf(ShelfController shelfController)
        {
            var shirtIndex = 0;
            for (int i = CurrentCarriage - 1; i >= 0; i--)
            {
                if (stack[i].animated) continue;
                shirtIndex = i;
                break;
            }

            shelfController.AddToShelf(stack[shirtIndex]);
            stack.RemoveAt(shirtIndex);
            // UpdateBlockPositions();
        }
    }
}