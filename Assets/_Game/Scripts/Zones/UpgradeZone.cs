using UnityEngine;
using Aezakmi.UI;

namespace Aezakmi.Zones
{
    public class UpgradeZone : ZoneBase
    {
        [SerializeField] private LiftingUI activatePrinterLiftingUI;

        protected override void OnZoneLoaded() => activatePrinterLiftingUI.Open();

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            activatePrinterLiftingUI.Close();
        }
    }
}
