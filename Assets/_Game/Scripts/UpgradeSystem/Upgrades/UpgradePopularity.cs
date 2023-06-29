using System.Collections.Generic;
using UnityEngine;
using Aezakmi.CustomerSystem;

namespace Aezakmi.UpgradeSystem
{
    public class UpgradePopularity : UpgradeBase
    {
        public override int NextLevelCost => (level + 1) * 12;

        [SerializeField] private List<float> cooldownPerLevel;

        public override void OnUpgrade() => CustomersGenerationManager.Instance.generationCooldown = cooldownPerLevel[level];
    }
}