using UnityEngine;

namespace Aezakmi.PrintSystem.Patterns
{
    [CreateAssetMenu(fileName = ("PatternPreset"), menuName = "Aezakmi/Pattern Preset")]
    public class PatternPreset : ScriptableObject
    {
        public Sprite patternSpriteUI;
        public GameObject patternPrefab;
        public Texture2D patternMask;
    }
}