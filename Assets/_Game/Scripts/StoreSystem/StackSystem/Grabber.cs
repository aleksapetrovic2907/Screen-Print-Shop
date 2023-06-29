using UnityEngine;

namespace Aezakmi.StoreSystem.StackSystem
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private Vector3 center;
        [SerializeField] private float radius;
        [SerializeField] private bool scaleWithTransform;
        [SerializeField] private LayerMask layerMask;

        private Carrier m_carrier;

        private void Start() => m_carrier = GetComponent<Carrier>();

        private void Update()
        {
            var r = scaleWithTransform ? radius * transform.localScale.x : radius;
            var c = scaleWithTransform ? center * transform.localScale.x : center;
            Collider[] shirts = Physics.OverlapSphere(c + transform.position, r, layerMask);
            if (shirts.Length == 0) return;

            foreach (var shirt in shirts)
            {
                var stackableShirt = shirt.GetComponent<StackableShirt>();
                if (stackableShirt == null) continue;

                m_carrier.AddShirtToStack(stackableShirt);
                stackableShirt.parentPrinter.ThrownShirtPickedUp();
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