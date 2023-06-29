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

            if (currentStoreShelfWanderPoint == null)
            {
                m_customerController.LeaveDueToEmptyStore();
                return;
            }

            m_aiPath.destination = currentStoreShelfWanderPoint.GetRandomPointInArea();
            m_aiPath.canMove = true;
        }

        public void ReachedShelf()
        {
            m_aiPath.canMove = false;

            var lookPosition = (currentStoreShelfWanderPoint.shelfController.observePoint.position - transform.position).normalized;
            lookPosition.y = transform.position.y;
            var direction = Quaternion.LookRotation(lookPosition);
            transform.DORotateQuaternion(direction, s_rotateTowardsShelfDuration).SetEase(Ease.OutSine).Play();

            TimersManager.SetTimer(this, CustomerBehaviourParameters.Instance.GetObserveTime(), delegate { m_customerController.ObservedShelf(); });
        }
    }
}