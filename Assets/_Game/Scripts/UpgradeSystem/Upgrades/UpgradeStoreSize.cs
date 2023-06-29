using System.Collections.Generic;
using UnityEngine;
using Aezakmi.StoreSystem;
using Aezakmi.CustomerSystem;

namespace Aezakmi.UpgradeSystem
{
    public class UpgradeStoreSize : UpgradeBase
    {
        public override int NextLevelCost => (level + 1) * 500;

        [SerializeField] private List<int> maxCustomersPerShopLevel;

        public override void OnUpgrade()
        {
            CustomersGenerationManager.Instance.maxCustomers = maxCustomersPerShopLevel[level];
            StoreSizeManager.Instance.UpgradeStoreSize(level);
        }
    }
}