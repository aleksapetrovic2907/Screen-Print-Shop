using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PaintIn3D;
using Aezakmi.Transitions;
using Aezakmi.PrintSystem.Selections;

namespace Aezakmi.PrintSystem.Patterns
{
    public class PatternSelectionManager : GloballyAccessibleBase<PatternSelectionManager>
    {
        public Material ShirtMaterial => m_shirtPaintableTexture.GetComponent<Renderer>().material;

        [SerializeField] private List<PatternPreset> patternPresets;

        [SerializeField] private GameObject patternButtonPrefab;
        [SerializeField] private Transform patternButtonsContent;

        [SerializeField] private Vector2 shirtSaturationRange, shirtBrightnessRange;

        // Used for positioning (via tweens) of the patterns when selecting.
        [SerializeField] private Transform instantiateTransform;
        [SerializeField] private Transform destroyTransform;
        [SerializeField] private Transform middleTransform;
        [SerializeField] private Transform insideWingTransform;
        [SerializeField] private float middleDuration;
        [SerializeField] private float edgesDuration;

        [SerializeField] private FadeScreenTransitionAnimation moveToColorSelectionTransition;

        private bool m_patternsCurrentlyAnimated = false;
        private PatternPreset m_selectedPatternPreset = null;
        private GameObject m_selectedPattern = null;
        private P3dPaintableTexture m_shirtPaintableTexture;

        private List<GameObject> m_disposablePatterns = new List<GameObject>();

        protected override void Awake()
        {
            base.Awake();
            InstantiateButtons();
        }

        public void InstantiateShirt()
        {
            var shirt = Instantiate(MerchandiseSelectionManager.Instance.SelectedMerchandisePreset.flat);
            m_shirtPaintableTexture = shirt.GetComponent<P3dPaintableTexture>();

            SolidPainter.Paint(shirt.GetComponent<Renderer>().material, Random.ColorHSV
            (
                0, 1,
                shirtSaturationRange.x, shirtSaturationRange.y,
                shirtBrightnessRange.x, shirtBrightnessRange.y
            ));
        }

        private void InstantiateButtons()
        {
            for (int i = 0; i < patternPresets.Count; i++)
            {
                var index = i;

                var pButton = Instantiate(patternButtonPrefab, patternButtonsContent).GetComponent<ButtonUI>();
                pButton.button.onClick.AddListener(delegate { SelectPattern(index); });
                pButton.image.sprite = patternPresets[i].patternSpriteUI;
            }
        }

        public void SelectPattern(int index)
        {
            if (m_patternsCurrentlyAnimated) return;
            if (patternPresets[index] == m_selectedPatternPreset) return;

            if (m_selectedPatternPreset != null)
            {
                MoveCurrentPatternToDestruction();
                m_selectedPatternPreset = patternPresets[index];
            }
            else
            {
                m_selectedPatternPreset = patternPresets[index];
                PlaceNewPattern();
            }

            m_patternsCurrentlyAnimated = true;
        }

        private void MoveCurrentPatternToDestruction()
        {
            var oldPattern = m_selectedPattern;

            Sequence moveToDestruction = DOTween.Sequence();
            Tween moveToMiddle = oldPattern.transform.DOMove(middleTransform.position, middleDuration).SetEase(Ease.InOutSine).OnComplete(PlaceNewPattern);
            Tween moveToEdge = oldPattern.transform.DOMove(destroyTransform.position, edgesDuration).SetEase(Ease.InOutSine);
            moveToDestruction.Append(moveToMiddle).Append(moveToEdge).OnComplete(delegate { Destroy(oldPattern); }).Play();
        }

        private void PlaceNewPattern()
        {
            m_selectedPattern = Instantiate(m_selectedPatternPreset.patternPrefab, instantiateTransform.position, m_selectedPatternPreset.patternPrefab.transform.rotation);
            m_disposablePatterns.Add(m_selectedPattern.gameObject);

            Sequence moveToWing = DOTween.Sequence();
            Tween moveFromEdge = m_selectedPattern.transform.DOMove(middleTransform.position, edgesDuration).SetEase(Ease.InOutSine);
            Tween moveFromMiddle = m_selectedPattern.transform.DOMove(insideWingTransform.position, middleDuration).SetEase(Ease.InOutSine);
            moveToWing.Append(moveFromEdge).Append(moveFromMiddle).OnComplete(delegate { m_patternsCurrentlyAnimated = false; }).Play();
        }

        public void FinishedSelection()
        {
            m_shirtPaintableTexture.LocalMaskTexture = m_selectedPatternPreset.patternMask;
            m_selectedPattern.transform.parent = PrintingMachineTurnManager.Instance.CurrentWing;
            moveToColorSelectionTransition.StartTransition();

            m_selectedPatternPreset = null;
            m_selectedPattern = null;
        }

        public void DestroyShirt()
        {
            foreach (var disposablePattern in m_disposablePatterns)
            {
                if (disposablePattern != null)
                    Destroy(disposablePattern);
            }

            m_shirtPaintableTexture.gameObject.SetActive(false);
            m_shirtPaintableTexture = null;
        }
    }
}