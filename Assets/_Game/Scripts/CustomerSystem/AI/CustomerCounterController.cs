using UnityEngine;
using Pathfinding;
using Aezakmi.StoreSystem;

namespace Aezakmi.CustomerSystem.AI
{
    public class CustomerCounterController : MonoBehaviour
    {
        public bool ReachedQueuePosition { get; private set; } = false;

        private AIPath m_aiPath;
        private CustomerController m_customerController;
        private StoreCounter m_storeCounter;

        private void Awake()
        {
            m_aiPath = GetComponent<AIPath>();
            m_customerController = GetComponent<CustomerController>();
        }

        private void Update()
        {
            if (m_aiPath.reachedDestination)
            {
                m_aiPath.canMove = false;
                ReachedQueuePosition = true;
            }
        }

        public void GetStoreCounter()
        {
            m_storeCounter = StoreCountersManager.Instance.GetStoreCounter();
            m_storeCounter.AddCustomer(this);
        }

        public void MoveInQueue(Vector3 position)
        {
            m_aiPath.canMove = true;
            m_aiPath.destination = position;
        }
    }
}