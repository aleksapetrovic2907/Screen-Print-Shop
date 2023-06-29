using UnityEngine;
using Aezakmi.StoreSystem.StackSystem;

namespace Aezakmi.StoreSystem
{
    public class StoreShelfWanderPoint : MonoBehaviour
    {
        public ShelfController shelfController;

        [SerializeField] private Vector3 size = Vector3.one;
        [SerializeField] private Vector3 offset = Vector3.zero;

        public Vector3 GetRandomPointInArea()
        {
            var x = Random.Range(-size.x, size.x) / 2f;
            var z = Random.Range(-size.z, size.z) / 2f;

            var point = transform.position + offset + new Vector3(x, 0, z);

            return point;
        }

#if UNITY_EDITOR
        [SerializeField] private bool drawGizmos = false;
        [SerializeField] private Color gizmosColor = Color.blue;
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            Gizmos.color = gizmosColor;
            Gizmos.DrawWireCube(transform.position + offset, size);
        }
#endif
    }
}