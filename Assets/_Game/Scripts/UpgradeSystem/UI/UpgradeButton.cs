using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi.UpgradeSystem.UI
{
    [RequireComponent(typeof(Button))]
    public class UpgradeButton : MonoBehaviour
    {
        public Button button;

        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI maxed;

        private const string LEVEL_PREFIX = "LVL ";

        public void SetInteractibility(bool interactable)
        {
            button.interactable = interactable;
        }

        public void UpdateUI(int newLevel, int newCost, bool isMaxed)
        {
            if (isMaxed)
            {
                maxed.gameObject.SetActive(true);
                level.gameObject.SetActive(false);
                cost.gameObject.SetActive(false);

                return;
            }

            level.text = LEVEL_PREFIX + (newLevel + 1);
            cost.text = newCost.ToString();
        }
    }
}