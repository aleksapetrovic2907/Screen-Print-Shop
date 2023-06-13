using UnityEngine;
using Pathfinding;
using Aezakmi.StoreSystem;

namespace Aezakmi.CustomerSystem.AI
{
    public enum StoreAccessingStates
    { EnteringStore, LeavingStore }

    public class CustomerStoreAccesswayController : MonoBehaviour
    {
        public StoreAccessingStates storeAccessingState;

        private AIPath m_aiPath;
        private CustomerController m_customerController;
        private Vector3 m_targetPosition;

        private void Awake()
        {
            m_aiPath = GetComponent<AIPath>();
            m_customerController = GetComponent<CustomerController>();
        }

        private void Update()
        {
            if (!m_aiPath.reachedDestination) return;
            m_customerController.ReachedAccessway();
        }

        public void EnterStore()
        {
            storeAccessingState = StoreAccessingStates.EnteringStore;
            m_targetPosition = StoreAccesswaysManager.Instance.GetRandomEntrance();
            m_aiPath.destination = m_targetPosition;
        }

        public void LeaveStore()
        {
            storeAccessingState = StoreAccessingStates.LeavingStore;
            m_targetPosition = StoreAccesswaysManager.Instance.GetRandomExit();
            m_aiPath.destination = m_targetPosition;
        }
    }
}