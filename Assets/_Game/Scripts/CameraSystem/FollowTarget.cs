using UnityEngine;

namespace Aezakmi.CameraSystem
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;

        [SerializeField] private float speed;
        [SerializeField] private Vector3 offset;

        private void LateUpdate()
        {
            if (target == null) return;
            transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);
        }
    }
}