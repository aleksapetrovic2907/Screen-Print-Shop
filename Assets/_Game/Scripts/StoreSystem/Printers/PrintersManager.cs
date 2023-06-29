using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Zones;

namespace Aezakmi.StoreSystem.Printers
{
    public class PrintersManager : GloballyAccessibleBase<PrintersManager>
    {
        public PrinterController currentPrinter = null;
        public ActivatePrinterZone currentPrinterZone = null;
        public List<PrinterController> printerControllers;

        public void TogglePrinterWings()
        {
            foreach (var pc in printerControllers)
                pc.ToggleWings();
        }
    }
}