using System.Collections.Generic;
using Aezakmi.UI;

namespace Aezakmi.UpgradeSystem
{
    public class UpgradesManager : GloballyAccessibleBase<UpgradesManager>
    {
        public List<UpgradeBase> upgrades;

        private void Start()
        {
            // LoadSaveData();
            foreach (var upgrade in upgrades)
                upgrade.OnUpgrade();

            ConnectButtonsToUpgrades();
            UpdateButtonsUI();
            UpdateButtonsInteractibility();
        }

        private void ConnectButtonsToUpgrades()
        {
            foreach (var upgrade in upgrades)
            {
                upgrade.upgradeButton.button.onClick.AddListener(delegate
                {
                    TryUpgrade(upgrade);
                });
            }
        }

        private void TryUpgrade(UpgradeBase upgrade)
        {
            if (upgrade.Maxed) return;
            if (GameManager.Instance.money < upgrade.NextLevelCost) return;

            GameManager.Instance.money -= upgrade.NextLevelCost;
            MoneyUI.Instance.UpdateValue();
            MoneyUI.Instance.DoScale();

            upgrade.level++;
            upgrade.OnUpgrade();
            upgrade.upgradeButton.UpdateUI(upgrade.level, upgrade.NextLevelCost, upgrade.Maxed);
            UpdateButtonsInteractibility();
        }

        // Sets interactibility of each upgradeButton depending on
        // whether the upgrade is maxed out and is affordable.
        // Call this each time you gain/lose money.
        public void UpdateButtonsInteractibility()
        {
            foreach (var upgrade in upgrades)
            {
                bool interactable = !upgrade.Maxed && GameManager.Instance.money >= upgrade.NextLevelCost;
                upgrade.upgradeButton.SetInteractibility(interactable);
            }
        }

        public void UpdateButtonsUI()
        {
            foreach (var upgrade in upgrades)
                upgrade.upgradeButton.UpdateUI(upgrade.level, upgrade.NextLevelCost, upgrade.Maxed);
        }
    }
}