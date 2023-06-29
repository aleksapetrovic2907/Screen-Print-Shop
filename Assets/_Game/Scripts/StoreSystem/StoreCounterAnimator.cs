using UnityEngine;
using TMPro;
using DG.Tweening;
using NaughtyAttributes;

namespace Aezakmi.StoreSystem
{
    public class StoreCounterAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject directionalMoneyPrefab;
        [SerializeField] private Vector3 directionalMoneyLocalPosition;
        [SerializeField] private Transform cashRegister;
        [SerializeField] private float shakeDuration = .5f;
        [SerializeField] private float shakeStrength = 90;
        [SerializeField] private TextMeshPro timerText;

        private StoreCounter m_storeCounter;

        private void Start()
        {
            m_storeCounter = GetComponent<StoreCounter>();
        }

        public void ItemSold()
        {
            var money = Instantiate(directionalMoneyPrefab, transform);
            money.transform.localPosition = directionalMoneyLocalPosition;

            cashRegister.DOShakeRotation(shakeDuration, shakeStrength, 10, 90, true, ShakeRandomnessMode.Harmonic).Play();
        }

        private void LateUpdate()
        {
            if(!m_storeCounter.isSupervised || m_storeCounter.CustomersCount == 0)
            {
                timerText.gameObject.SetActive(false);
            }
            else
            {
                timerText.gameObject.SetActive(true);
                timerText.text = m_storeCounter.TimeLeft.ToString("0.##") + "s";
            }
        }
    }
}