using UnityEngine;

namespace Aezakmi.StoreSystem.StackSystem
{
    public class FullIndicator : MonoBehaviour
    {
        [SerializeField] private Carrier carrier;
        [SerializeField] private GameObject indicatorSprite;

        private void LateUpdate() => indicatorSprite.SetActive(carrier.IsFull);
    }
}