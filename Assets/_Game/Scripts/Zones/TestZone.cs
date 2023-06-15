using UnityEngine;

namespace Aezakmi.Zones
{
    public class TestZone : ZoneBase
    {
        protected override void OnZoneLoaded()
        {
            Debug.Log("Zone Loaded.");
        }
    }
}