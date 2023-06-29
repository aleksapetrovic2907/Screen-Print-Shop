using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.StoreSystem.StackSystem
{
    [RequireComponent(typeof(Carrier))]
    public class ShelfStacker : MonoBehaviour
    {
        [SerializeField] private Vector3 center;
        [SerializeField] private float radius;
        [SerializeField] private bool scaleWithTransform;
        [SerializeField] private LayerMask layerMask;

        private Carrier m_carrier;

        private void Start() => m_carrier = GetComponent<Carrier>();

        private void Update()
        {
            if (m_carrier.IsEmpty) return;

            var r = scaleWithTransform ? radius * transform.localScale.x : radius;
            var c = scaleWithTransform ? center * transform.localScale.x : center;
            Collider[] shelves = Physics.OverlapSphere(c + transform.position, r, layerMask);
            if (shelves.Length == 0) return;

            foreach (var shelf in shelves)
            {
                var shelfController = shelf.GetComponent<ShelfController>();
                if (shelfController == null) continue;

                m_carrier.AddShirtToShelf(shelfController);
            }
        }


#if UNITY_EDITOR
        [SerializeField] private bool drawGizmos = true;
        [SerializeField] private Color gizmosColor;
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;
            Gizmos.color = gizmosColor;
            var r = scaleWithTransform ? radius * transform.localScale.x : radius;
            var c = scaleWithTransform ? center * transform.localScale.x : center;
            Gizmos.DrawWireSphere(c + transform.position, r);
        }
#endif
    }
}