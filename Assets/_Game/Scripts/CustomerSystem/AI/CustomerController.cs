using UnityEngine;
using Aezakmi.StoreSystem.Printers;

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
        [HideInInspector] public PrinterController printerShirtBoughtFrom;

        private CustomerStoreAccesswayController m_customerStoreAccesswayController;
        private CustomerShoppingController m_customerShoppingController;
        private CustomerCounterController m_customerCounterController;
        private CustomerDeletionController m_customerDeletionController;
        private CustomerEmojisController m_customerEmojisController;

        private void Start()
        {
            m_customerStoreAccesswayController = GetComponent<CustomerStoreAccesswayController>();
            m_customerShoppingController = GetComponent<CustomerShoppingController>();
            m_customerCounterController = GetComponent<CustomerCounterController>();
            m_customerDeletionController = GetComponent<CustomerDeletionController>();
            m_customerEmojisController = GetComponent<CustomerEmojisController>();

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
                m_customerDeletionController.enabled = true;
                m_customerDeletionController.MoveToDeletionPoint();
            }
        }

        public void ObservedShelf()
        {
            var willBuy = CustomerBehaviourParameters.Instance.GetBuyDecision() && m_customerShoppingController.currentStoreShelfWanderPoint.shelfController.HasShirts;

            if (willBuy)
            {
                m_customerShoppingController.currentStoreShelfWanderPoint.shelfController.GiveToCustomer(this);
                m_customerCounterController.enabled = true;
                m_customerCounterController.GetStoreCounter();
                m_customerEmojisController.ShowHappyEmojis();
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
                    m_customerEmojisController.ShowAngryEmojis();
                }
            }
        }

        public void ItemBought()
        {
            GameManager.Instance.ItemSold();
            m_customerCounterController.enabled = false;
            m_customerStoreAccesswayController.enabled = true;
            m_customerStoreAccesswayController.LeaveStore();
            m_customerEmojisController.ShowHappyEmojis();
        }

        public void LeaveDueToEmptyStore()
        {
            m_customerShoppingController.enabled = false;
            m_customerStoreAccesswayController.enabled = true;
            m_customerStoreAccesswayController.LeaveStore();
        }
    }
}