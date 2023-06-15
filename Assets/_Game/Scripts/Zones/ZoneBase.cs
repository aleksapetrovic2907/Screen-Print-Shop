using UnityEngine;

namespace Aezakmi.Zones
{
    [RequireComponent(typeof(Collider))]
    public abstract class ZoneBase : MonoBehaviour
    {
        public float ProgressNormalized => m_timer / duration;

        [SerializeField] private float duration;

        private float m_timer = 0f;
        private bool m_zoneEntered = false;

        protected virtual void Update()
        {
            if (m_zoneEntered) m_timer += Time.deltaTime;
            if (ProgressNormalized >= 1f)
            {
                m_zoneEntered = false;
                m_timer = 0f;

                OnZoneLoaded();
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.PLAYER)) return;
            m_zoneEntered = true;
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PLAYER)) return;
            m_zoneEntered = false;
            m_timer = 0f;
        }

        protected abstract void OnZoneLoaded();
    }
}