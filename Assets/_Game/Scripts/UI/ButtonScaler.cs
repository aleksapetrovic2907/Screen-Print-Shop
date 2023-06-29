using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Aezakmi.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonScaler : MonoBehaviour
    {
        [SerializeField] private float scaleBy;
        [SerializeField] private float duration = .07f;
        [SerializeField] private Ease ease = Ease.InOutSine;

        private Button m_button;
        private Tween m_tween;

        private void Awake()
        {
            m_button = GetComponent<Button>();
            m_button.onClick.AddListener(Scale);
        }

        private void Scale()
        {
            if (m_tween != null) m_tween.Rewind();
            m_tween = transform.DOScale(scaleBy * Vector3.one, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease).SetRelative(true).Play();
        }
    }
}