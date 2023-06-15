using System.Collections.Generic;
using UnityEngine;
using Aezakmi.CustomerSystem.AI;

namespace Aezakmi.StoreSystem
{
    public class StoreCounter : MonoBehaviour
    {
        public int CustomersCount => customersInQueue.Count;
        public List<CustomerCounterController> customersInQueue;
        public bool isSupervised = false;

        [SerializeField] private Transform queueFirstSpot;
        [SerializeField] private Vector3 offsetPerCustomer;

        private float m_timer = 0f;

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
            customersInQueue.Add(customer);

            customer.MoveInQueue(queueFirstSpot.position + offsetPerCustomer * CustomersCount);
        }

        private void SellItem()
        {
            customersInQueue[0].GetComponent<CustomerController>().ItemBought();
            customersInQueue.RemoveAt(0);
            MoveAllCustomers();
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