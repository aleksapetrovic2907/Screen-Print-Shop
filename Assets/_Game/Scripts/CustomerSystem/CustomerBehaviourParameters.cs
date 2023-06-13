using UnityEngine;

namespace Aezakmi.CustomerSystem
{
    public class CustomerBehaviourParameters : GloballyAccessibleBase<CustomerBehaviourParameters>
    {
        [SerializeField] private Vector2 minMaxObserveTime;
        [Range(0f, 1f)][SerializeField] private float buyPercentage;

        public float GetObserveTime() => Random.Range(minMaxObserveTime.x, minMaxObserveTime.y);
        public bool GetBuyDecision() => Random.Range(0f, 1f) <= buyPercentage;
    }
}