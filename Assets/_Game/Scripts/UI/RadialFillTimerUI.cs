using UnityEngine;
using Aezakmi.StoreSystem;

namespace Aezakmi.UI
{
    public class RadialFillTimerUI : MonoBehaviour
    {
        [SerializeField] private StoreCounter storeCounter;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void LateUpdate()
        {
            var angle = storeCounter.TimePassedNormalized * 360f;
            spriteRenderer.material.SetInt("_Arc1", Mathf.CeilToInt(angle));
        }
    }
}