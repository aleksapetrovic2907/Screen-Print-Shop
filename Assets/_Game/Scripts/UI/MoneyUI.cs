using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Aezakmi.Tweens;

namespace Aezakmi.UI
{
    public class MoneyUI : GloballyAccessibleBase<MoneyUI>
    {
        [SerializeField] private TextMeshProUGUI moneyValue;
        [SerializeField] private GameObject moneyIcon;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Scale scale;

        public void UpdateValue() => moneyValue.text = GameManager.Instance.money.ToString();
        public void DoScale()
        {
            scale.Rewind();
            scale.PlayTween();
        }

        public void SetInvisible()
        {
            backgroundImage.enabled = false;
            moneyValue.gameObject.SetActive(false);
            moneyIcon.SetActive(false);
        }

        public void SetVisible()
        {
            backgroundImage.enabled = true;
            moneyValue.gameObject.SetActive(true);
            moneyIcon.SetActive(true);
        }
    }
}