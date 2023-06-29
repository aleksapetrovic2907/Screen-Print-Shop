using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.StoreSystem
{
    public class StoreCountersManager : GloballyAccessibleBase<StoreCountersManager>
    {
        public float sellDuration;

        [SerializeField] private List<StoreCounter> storeCounters;

        public StoreCounter GetStoreCounter()
        {
            int leastOccupiedCounterIndex = 0;

            for (int i = 0; i < storeCounters.Count; i++)
            {
                if (!storeCounters[i].gameObject.activeSelf) continue;
                if (storeCounters[i].CustomersCount >= storeCounters[leastOccupiedCounterIndex].CustomersCount) continue;
                leastOccupiedCounterIndex = i;
            }

            return storeCounters[leastOccupiedCounterIndex];
        }
    }
}