using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.StoreSystem
{
    public class StoreShelfWanderPointsManager : GloballyAccessibleBase<StoreShelfWanderPointsManager>
    {
        [SerializeField] private List<StoreShelfWanderPoint> storeShelfWanderPoints;

        public StoreShelfWanderPoint? GetRandomShelfWanderPoint()
        {
            List<StoreShelfWanderPoint> nonEmptyShelfWanderPoints = new List<StoreShelfWanderPoint>();

            foreach (var wanderPoint in storeShelfWanderPoints)
            {
                if (!wanderPoint.shelfController.HasShirts || !wanderPoint.gameObject.activeSelf) continue;
                nonEmptyShelfWanderPoints.Add(wanderPoint);
            }

            if (nonEmptyShelfWanderPoints.Count == 0) return null;

            return nonEmptyShelfWanderPoints[Random.Range(0, nonEmptyShelfWanderPoints.Count)];
        }
    }
}