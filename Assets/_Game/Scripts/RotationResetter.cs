using UnityEngine;

namespace Aezakmi
{
    public class RotationResetter : MonoBehaviour
    {
        public Vector3 euler;
        private void LateUpdate() => transform.eulerAngles = euler;
    }
}