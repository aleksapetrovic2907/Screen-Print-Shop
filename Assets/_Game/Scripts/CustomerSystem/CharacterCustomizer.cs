using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.CustomerSystem
{
    public class CharacterCustomizer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> heads;
        [SerializeField] private List<GameObject> hairs;
        [SerializeField] private List<GameObject> glasses;
        [SerializeField] private List<GameObject> torsos;
        [SerializeField] private List<GameObject> legs;

        private bool m_wearsGlasses = false;

        private void Start()
        {
            DisablePlaceholders();
            
            var randomHead = Random.Range(0, heads.Count);
            heads[randomHead].SetActive(true);

            var randomHair = Random.Range(0, hairs.Count);
            hairs[randomHair].SetActive(true);

            var randomTorsos = Random.Range(0, torsos.Count);
            torsos[randomTorsos].SetActive(true);

            var randomLegs = Random.Range(0, legs.Count);
            legs[randomLegs].SetActive(true);

            m_wearsGlasses = Random.Range(0f, 1f) < CustomizationManager.Instance.spectaclesPercentage;

            if (m_wearsGlasses)
            {
                var randomGlasses = Random.Range(0, glasses.Count);
                glasses[randomGlasses].SetActive(true);
            }
        }

        private void DisablePlaceholders()
        {
            heads[0].SetActive(false);
            hairs[0].SetActive(false);
            glasses[0].SetActive(false);
            torsos[0].SetActive(false);
            legs[0].SetActive(false);
        }
    }
}