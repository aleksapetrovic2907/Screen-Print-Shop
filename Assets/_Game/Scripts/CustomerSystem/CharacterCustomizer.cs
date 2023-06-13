using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.CustomerSystem
{
    public enum Gender
    { Male, Female }

    public class CharacterCustomizer : MonoBehaviour
    {
        public Gender gender;

        [SerializeField] private List<GameObject> maleHeads;
        [SerializeField] private List<GameObject> femaleHeads;

        [SerializeField] private List<GameObject> maleHairs;
        [SerializeField] private List<GameObject> femaleHairs;

        [SerializeField] private List<GameObject> maleGlasses;
        [SerializeField] private List<GameObject> femaleGlasses;

        [SerializeField] private List<GameObject> legs;

        private bool m_wearsGlasses = false;

        private void Start()
        {
            SetGender();

            var heads = gender == Gender.Male ? maleHeads : femaleHeads;
            var hairs = gender == Gender.Male ? maleHairs : femaleHairs;

            var randomHead = Random.Range(0, heads.Count);
            heads[randomHead].SetActive(true);

            var randomHair = Random.Range(0, hairs.Count);
            hairs[randomHair].SetActive(true);

            var randomLegs = Random.Range(0, legs.Count);
            legs[randomLegs].SetActive(true);

            m_wearsGlasses = Random.Range(0f, 1f) < CustomizationManager.Instance.spectaclesPercentage;
            if (m_wearsGlasses)
            {
                var glasses = gender == Gender.Male ? maleGlasses : femaleGlasses;
                var randomGlasses = Random.Range(0, glasses.Count);
                glasses[randomGlasses].SetActive(true);
            }
        }

        private void SetGender()
        {
            gender = Random.Range(0f, 1f) < CustomizationManager.Instance.malesPercentage ? Gender.Male : Gender.Female;
        }
    }
}