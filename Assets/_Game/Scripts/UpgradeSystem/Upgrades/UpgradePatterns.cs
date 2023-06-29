using Aezakmi.StoreSystem.Printers;

namespace Aezakmi.UpgradeSystem
{
    public class UpgradePatterns : UpgradeBase
    {
        public override int NextLevelCost => (level + 1) * 100;

        public override void OnUpgrade()
        {
            GameManager.Instance.patternsCount = level + 1;
            PrintersManager.Instance.TogglePrinterWings();
        }
    }
}