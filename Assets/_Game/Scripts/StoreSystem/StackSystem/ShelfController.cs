using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Aezakmi.CustomerSystem.AI;

namespace Aezakmi.StoreSystem.StackSystem
{
    public class ShelfController : MonoBehaviour
    {
        public bool HasShirts => m_piledShirts.Count > 0;

        public Transform observePoint; // The point the customers will look at when they reach the given wander point.

        [SerializeField] private Transform firstSlot;
        [SerializeField] private Vector3 shirtEuler;
        [SerializeField] private float heightMargin = .03f;
        [SerializeField] private Vector3Int dimensions;
        [SerializeField] private float animationDuration;

        private List<StackableShirt> m_piledShirts = new List<StackableShirt>();
        private int m_count = 0;

        public void AddToShelf(StackableShirt stackableShirt)
        {
            m_piledShirts.Add(stackableShirt);
            stackableShirt.transform.parent = null;

            stackableShirt.animated = true;
            Sequence throwSequence = DOTween.Sequence();

            Tween moveToPosition = stackableShirt.transform.DOJump(GetPositionOnShelf(stackableShirt), 1.5f, 1, animationDuration).SetEase(Ease.InOutSine);
            Tween rotate = stackableShirt.transform.DORotate(shirtEuler, animationDuration).SetEase(Ease.InOutSine);

            throwSequence.Append(moveToPosition).Join(rotate).OnComplete(delegate { stackableShirt.animated = false; }).Play();
            m_count++;
        }

        private Vector3 GetPositionOnShelf(StackableShirt shirt)
        {
            var z = (m_count / dimensions.x) % dimensions.y;
            var x = m_count % dimensions.x;
            var y = m_count / (dimensions.x * dimensions.y);

            var targetPosition = firstSlot.position + new Vector3(x * shirt.size.x, y * shirt.size.y, z * shirt.size.z);

            return targetPosition;
        }

        public void GiveToCustomer(CustomerController customerController)
        {
            var lastShirtIndex = m_piledShirts.Count - 1;
            customerController.printerShirtBoughtFrom = m_piledShirts[lastShirtIndex].parentPrinter;

            Destroy(m_piledShirts[lastShirtIndex].gameObject);
            m_piledShirts.RemoveAt(lastShirtIndex);
            m_count--;
        }
    }
}