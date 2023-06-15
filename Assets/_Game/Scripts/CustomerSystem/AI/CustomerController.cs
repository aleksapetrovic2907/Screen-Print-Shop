using UnityEngine;

namespace Aezakmi.CustomerSystem.AI
{
    /*
        * Upon being instantiated the customer enters the store.
        * Upon entering the store, the customer goes shopping.
            * If the customer doesn't find anything interesting, he leaves the store.
            * If the customer finds merchandise he finds interesting, he picks it out and goes to the counter.
                * At the counterline he waits for the cashier to sell him the merchandise after which he leaves the store.
        * After leaving the store, the customer goes toward a deletion point.
    */

    public class CustomerController : MonoBehaviour
    {
        private CustomerStoreAccesswayController m_customerStoreAccesswayController;
        private CustomerShoppingController m_customerShoppingController;
        private CustomerCounterController m_customerCounterController;

        private void Start()
        {
            m_customerStoreAccesswayController = GetComponent<CustomerStoreAccesswayController>();
            m_customerShoppingController = GetComponent<CustomerShoppingController>();
            m_customerCounterController = GetComponent<CustomerCounterController>();

            m_customerStoreAccesswayController.EnterStore();
        }

        public void ReachedAccessway()
        {
            m_customerStoreAccesswayController.enabled = false;

            if (m_customerStoreAccesswayController.storeAccessingState == StoreAccessingStates.EnteringStore)
            {
                m_customerShoppingController.enabled = true;
                m_customerShoppingController.MoveToShelf();
            }
            else
            {
                // todo: move to deletion point
            }
        }

        public void ObservedShelf()
        {
            var willBuy = CustomerBehaviourParameters.Instance.GetBuyDecision();

            if (willBuy)
            {
                m_customerCounterController.enabled = true;
                m_customerCounterController.GetStoreCounter();
            }
            else
            {
                var willContinueShopping = CustomerBehaviourParameters.Instance.GetContinueShoppingDecision();

                if (willContinueShopping)
                {
                    m_customerShoppingController.enabled = true;
                    m_customerShoppingController.MoveToShelf();
                }
                else
                {
                    m_customerStoreAccesswayController.enabled = true;
                    m_customerStoreAccesswayController.LeaveStore();
                }
            }
        }

        public void ItemBought()
        {
            m_customerStoreAccesswayController.enabled = true;
            m_customerStoreAccesswayController.LeaveStore();
        }
    }
}