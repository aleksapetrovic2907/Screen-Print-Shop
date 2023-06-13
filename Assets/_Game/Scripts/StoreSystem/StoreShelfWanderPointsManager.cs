using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.StoreSystem
{
    public class StoreShelfWanderPointsManager : GloballyAccessibleBase<StoreShelfWanderPointsManager>
    {
        [SerializeField] private List<StoreShelfWanderPoint> storeShelfWanderPoints;

        public StoreShelfWanderPoint GetRandomShelfWanderPoint() => storeShelfWanderPoints[Random.Range(0, storeShelfWanderPoints.Count)];
    }
}