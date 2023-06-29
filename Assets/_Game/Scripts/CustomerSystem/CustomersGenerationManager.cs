using System.Collections.Generic;
using UnityEngine;
using Aezakmi.CustomerSystem.AI;
using Aezakmi.StoreSystem.StackSystem;

namespace Aezakmi.CustomerSystem
{
    public class CustomersGenerationManager : GloballyAccessibleBase<CustomersGenerationManager>
    {
        public bool IsStoreEmpty
        {
            get
            {
                var empty = true;

                foreach (var shelfController in shelfControllers)
                {
                    if (shelfController.HasShirts)
                    {
                        empty = false;
                        break;
                    }
                }

                return empty;
            }
        }

        public int maxCustomers;
        public List<CustomerController> customerControllers;
        public float generationCooldown;

        [SerializeField] private GameObject maleCustomerPrefab;
        [SerializeField] private GameObject femaleCustomerPrefab;
        [SerializeField] private Transform generationPoint;
        [SerializeField] private List<ShelfController> shelfControllers;

        private float m_timer = 0f;

        protected override void Awake()
        {
            base.Awake();
            m_timer = generationCooldown;
        }

        private void Update()
        {
            m_timer += Time.deltaTime;

            if (IsStoreEmpty) return;
            if (customerControllers.Count >= maxCustomers) return;
            if (m_timer < generationCooldown) return;

            m_timer = 0f;
            GenerateCustomer();
        }

        private void GenerateCustomer()
        {
            var customerPrefab = Random.Range(0f, 1f) <= CustomizationManager.Instance.malesPercentage ? maleCustomerPrefab : femaleCustomerPrefab;
            var customerController = Instantiate(customerPrefab, generationPoint.position, customerPrefab.transform.rotation).GetComponent<CustomerController>();
            customerControllers.Add(customerController);
        }

        public void DeleteCustomer(CustomerController cc)
        {
            customerControllers.Remove(cc);
            Destroy(cc.gameObject);
        }
    }
}