using UnityEngine;
using Pathfinding;
using DG.Tweening;
using Timers;
using Aezakmi.StoreSystem;

namespace Aezakmi.CustomerSystem.AI
{
    public class CustomerShoppingController : MonoBehaviour
    {
        public StoreShelfWanderPoint currentStoreShelfWanderPoint;

        private AIPath m_aiPath;
        private CustomerController m_customerController;

        private static float s_rotateTowardsShelfDuration = .5f;

        private void Awake()
        {
            m_aiPath = GetComponent<AIPath>();
            m_customerController = GetComponent<CustomerController>();
        }

        private void Update()
        {
            if (!m_aiPath.reachedDestination) return;

            ReachedShelf();
            enabled = false;
        }

        public void MoveToShelf()
        {
            currentStoreShelfWanderPoint = StoreShelfWanderPointsManager.Instance.GetRandomShelfWanderPoint();
            m_aiPath.destination = currentStoreShelfWanderPoint.GetRandomPointInArea();
        }

        public void ReachedShelf()
        {
            m_aiPath.canMove = false;

            var lookPosition = (currentStoreShelfWanderPoint.observePoint.position - transform.position).normalized;
            lookPosition.y = transform.position.y;
            var direction = Quaternion.LookRotation(lookPosition);
            transform.DORotateQuaternion(direction, s_rotateTowardsShelfDuration).SetEase(Ease.OutSine).Play();

            TimersManager.SetTimer(this, CustomerBehaviourParameters.Instance.GetObserveTime(), delegate { m_customerController.ObservedShelf(); });
        }
    }
}