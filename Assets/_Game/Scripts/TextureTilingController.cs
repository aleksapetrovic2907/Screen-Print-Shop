using UnityEngine;
using NaughtyAttributes;
namespace Aezakmi
{
    [RequireComponent(typeof(Renderer))]
    public class TextureTilingController : MonoBehaviour
    {
        [SerializeField] private Vector2 ratio;

        private Renderer m_renderer;

        private void Awake() => SetTiling();

        // Sets tiling based on transforms scale.
        [Button]
        public void SetTiling()
        {
            m_renderer = GetComponent<Renderer>();
            Vector2 targetScale = new Vector2
            (
                transform.localScale.x * ratio.x,
                transform.localScale.z * ratio.y
            );

            m_renderer.material.mainTextureScale = targetScale;
        }
    }
}