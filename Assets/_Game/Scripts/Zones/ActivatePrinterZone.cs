using UnityEngine;
using Aezakmi.StoreSystem.Printers;
using Aezakmi.UI;

namespace Aezakmi.Zones
{
    public class ActivatePrinterZone : ZoneBase
    {
        [SerializeField] private LiftingUI activatePrinterLiftingUI;
        [SerializeField] private PrinterController printerController;

        protected override void OnZoneLoaded()
        {
            activatePrinterLiftingUI.Open();
            PrintersManager.Instance.currentPrinter = printerController;
            PrintersManager.Instance.currentPrinterZone = this;
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            activatePrinterLiftingUI.Close();

            PrintersManager.Instance.currentPrinter = null;
            PrintersManager.Instance.currentPrinterZone = null;
        }
    }
}