using UnityEngine;
using Aezakmi.UpgradeSystem.UI;

namespace Aezakmi.UpgradeSystem
{
    public abstract class UpgradeBase : MonoBehaviour
    {
        public abstract int NextLevelCost { get; }
        public bool Maxed { get { return level == maxLevel; } }

        public int level = 0;
        public int maxLevel = 6;
        public UpgradeButton upgradeButton;

        public abstract void OnUpgrade();
    }
}