using UnityEngine;

namespace Aezakmi.PrintSystem.Selections
{
    public class SelectableMerch : MonoBehaviour
    {
        public int index;

        [SerializeField] private Renderer merchRenderer;

        public void GetSelected()
        {
            merchRenderer.material.SetInt("_ActiveFresnel", 1);
        }

        public void GetDeselected()
        {
            merchRenderer.material.SetInt("_ActiveFresnel", 0);
        }
    }
}