using UnityEngine;

namespace Aezakmi
{
    public class FPSSetter : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate = 60;
        private void Awake() => Application.targetFrameRate = targetFrameRate;
    }
}