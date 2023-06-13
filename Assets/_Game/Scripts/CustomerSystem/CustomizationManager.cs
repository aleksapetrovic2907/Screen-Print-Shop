using UnityEngine;

namespace Aezakmi.CustomerSystem
{
    public class CustomizationManager : GloballyAccessibleBase<CustomizationManager>
    {
        [Range(0f, 1f)] public float malesPercentage = 0.5f;
        [Range(0f, 1f)] public float spectaclesPercentage = 0.5f;
    }
}