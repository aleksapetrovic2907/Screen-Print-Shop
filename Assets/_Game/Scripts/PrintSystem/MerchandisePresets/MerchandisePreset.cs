using UnityEngine;

namespace Aezakmi.PrintSystem
{
    [CreateAssetMenu(fileName = "Merchandise Preset", menuName = "Aezakmi/Merchandise Preset")]
    public class MerchandisePreset : ScriptableObject
    {
        public GameObject flat;
        public GameObject folded;
    }
}