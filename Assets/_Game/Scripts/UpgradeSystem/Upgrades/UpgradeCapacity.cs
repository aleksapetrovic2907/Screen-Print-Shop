using System.Collections.Generic;
using UnityEngine;
using Aezakmi.StoreSystem.StackSystem;

namespace Aezakmi.UpgradeSystem
{
    public class UpgradeCapacity : UpgradeBase
    {
        public override int NextLevelCost => (level + 1) * 34;

        [SerializeField] private Carrier playerCarrier;
        [SerializeField] private List<int> capacityPerLevel;

        public override void OnUpgrade()
        {
            playerCarrier.maxCapacity = capacityPerLevel[level];
        }
    }
}