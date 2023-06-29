using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Transitions;

namespace Aezakmi.PrintSystem.Selections
{
    public class MerchandiseSelectionManager : GloballyAccessibleBase<MerchandiseSelectionManager>
    {
        public MerchandisePreset SelectedMerchandisePreset => merchandisePresets[m_selectedMerch.index];
        public List<MerchandisePreset> merchandisePresets;

        [SerializeField] private Camera rayCamera;
        [SerializeField] private GameObject continueButton;
        [SerializeField] private FadeScreenTransitionAnimation moveToPrintingTransition;

        private SelectableMerch m_selectedMerch = null;

        private const uint MAX_RAY_DISTANCE = 10;

        private void Update()
        {
            CheckForSelection();
        }

        private void CheckForSelection()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (!Physics.Raycast(ray, out raycastHit, MAX_RAY_DISTANCE)) return;

            var selectable = raycastHit.collider.GetComponent<SelectableMerch>();
            if (selectable == null) return;
            if (m_selectedMerch != null && selectable != m_selectedMerch) m_selectedMerch.GetDeselected();

            m_selectedMerch = selectable;
            MerchandiseSelected();
        }

        private void MerchandiseSelected()
        {
            m_selectedMerch.GetSelected();
            continueButton.SetActive(true);
        }

        public void ContinueWithSelection()
        {
            m_selectedMerch.GetDeselected();
            continueButton.SetActive(false);
            moveToPrintingTransition.StartTransition();
        }
    }
}