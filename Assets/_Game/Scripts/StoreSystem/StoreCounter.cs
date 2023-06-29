using System.Collections.Generic;
using UnityEngine;
using Aezakmi.CustomerSystem.AI;

namespace Aezakmi.StoreSystem
{
    public class StoreCounter : MonoBehaviour
    {
        public float TimePassedNormalized => m_timer / StoreCountersManager.Instance.sellDuration;
        public float TimeLeft => StoreCountersManager.Instance.sellDuration - m_timer;
        public int CustomersCount => customersInQueue.Count;
        public List<CustomerCounterController> customersInQueue;
        public bool isSupervised = false;

        [SerializeField] private Transform queueFirstSpot;
        [SerializeField] private Vector3 offsetPerCustomer;

        private float m_timer = 0f;
        private StoreCounterAnimator m_storeCounterAnimator;

        private void Start()
        {
            m_storeCounterAnimator = GetComponent<StoreCounterAnimator>();
        }

        private void Update()
        {
            if (!isSupervised || CustomersCount == 0)
            {
                m_timer = 0f;
                return;
            }

            if (CustomersCount != 0 && !customersInQueue[0].ReachedQueuePosition)
            {
                m_timer = 0f;
                return;
            }

            m_timer += Time.deltaTime;

            if (m_timer >= StoreCountersManager.Instance.sellDuration)
            {
                m_timer = 0f;
                SellItem();
            }
        }

        public void AddCustomer(CustomerCounterController customer)
        {
            customer.MoveInQueue(queueFirstSpot.position + offsetPerCustomer * CustomersCount);
            customersInQueue.Add(customer);
        }

        private void SellItem()
        {
            customersInQueue[0].GetComponent<CustomerController>().ItemBought();
            customersInQueue.RemoveAt(0);
            MoveAllCustomers();
            m_storeCounterAnimator.ItemSold();
        }

        private void MoveAllCustomers()
        {
            for (int i = 0; i < CustomersCount; i++)
            {
                var position = queueFirstSpot.position + i * offsetPerCustomer;
                customersInQueue[i].MoveInQueue(position);
            }
        }
    }
}