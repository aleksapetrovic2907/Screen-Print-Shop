using UnityEngine;
using Pathfinding;
using Aezakmi.StoreSystem;

namespace Aezakmi.CustomerSystem.AI
{
    public class CustomerDeletionController : MonoBehaviour
    {
        private AIPath m_aiPath;
        private CustomerController m_customerController;

        private void Awake()
        {
            m_aiPath = GetComponent<AIPath>();
            m_customerController = GetComponent<CustomerController>();
        }

        private void Update()
        {
            if (!m_aiPath.reachedDestination) return;
            CustomersGenerationManager.Instance.DeleteCustomer(m_customerController);
        }

        public void MoveToDeletionPoint()
        {
            m_aiPath.destination = StoreAccesswaysManager.Instance.deletionPoint.position;
        }
    }
}