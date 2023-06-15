using UnityEngine;
using Aezakmi.StoreSystem;

namespace Aezakmi.Zones
{
    public class StoreCounterZone : ZoneBase
    {
        [SerializeField] private StoreCounter storeCounter;

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            storeCounter.isSupervised = false;
        }

        protected override void OnZoneLoaded() => storeCounter.isSupervised = true;
    }
}