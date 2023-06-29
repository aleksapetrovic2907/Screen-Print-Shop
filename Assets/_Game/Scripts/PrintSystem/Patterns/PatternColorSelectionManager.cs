using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using Aezakmi.Transitions;

namespace Aezakmi.PrintSystem.Patterns
{
    public class PatternColorSelectionManager : MonoBehaviour
    {
        public List<Color> colors;

        [SerializeField] private GameObject colorButtonPrefab;
        [SerializeField] private RectTransform colorsContent;
        [SerializeField] private Renderer ingotPainterMesh;
        [SerializeField] private P3dPaintDecal ingotPainterP3d;
        [SerializeField] private FadeScreenTransitionAnimation fadeToPainting;

        private void Awake()
        {
            for (int i = 0; i < colors.Count; i++)
            {
                var index = i;

                var cButton = Instantiate(colorButtonPrefab, colorsContent).GetComponent<ButtonUI>();
                cButton.image.color = colors[i];
                cButton.button.onClick.AddListener(delegate { ChangeColor(index); });
            }

            ChangeColor(0);
        }

        private void ChangeColor(int index)
        {
            ingotPainterMesh.material.color = colors[index];
            ingotPainterP3d.Color = colors[index];
        }

        public void FinishedColorSelection()
        {
            fadeToPainting.StartTransition();
        }
    }
}