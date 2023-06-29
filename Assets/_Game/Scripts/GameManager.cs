using UnityEngine;
using Aezakmi.Transitions;
using Aezakmi.PrintSystem.Selections;
using Aezakmi.PrintSystem.Patterns;
using Aezakmi.StoreSystem.Printers;
using Aezakmi.UpgradeSystem;
using Aezakmi.UI;

namespace Aezakmi
{
    public class GameManager : GloballyAccessibleBase<GameManager>
    {
        public int money = 0;
        public int patternsCount = 1;

        [SerializeField] private Vector2Int moneyGainRange;
        [SerializeField] private FadeScreenTransitionAnimation moveToIdleTransition;
        [SerializeField] private FadeScreenTransitionAnimation moveToPrintingTransition;

        public void GoToIdle()
        {
            PrintersManager.Instance.currentPrinter.ActivatePrinter(MerchandiseSelectionManager.Instance.SelectedMerchandisePreset, PatternSelectionManager.Instance.ShirtMaterial);
            PrintersManager.Instance.currentPrinter = null;

            Destroy(PrintersManager.Instance.currentPrinterZone.gameObject);
            PrintersManager.Instance.currentPrinterZone = null;

            moveToIdleTransition.StartTransition();
        }

        public void GoToPrinting()
        {
            moveToPrintingTransition.StartTransition();
        }

        public void ItemSold()
        {
            GetMoney();
            MoneyUI.Instance.UpdateValue();
            MoneyUI.Instance.DoScale();
            UpgradesManager.Instance.UpdateButtonsInteractibility();
        }

        public void GetMoney()
        {
            var moneyGained = Random.Range(moneyGainRange.x, moneyGainRange.y);
            money += moneyGained;
        }
    }
}