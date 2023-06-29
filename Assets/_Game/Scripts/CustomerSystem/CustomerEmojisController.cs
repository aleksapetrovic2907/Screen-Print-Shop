using System;
using UnityEngine;

namespace Aezakmi.CustomerSystem
{
    public class CustomerEmojisController : MonoBehaviour
    {
        [SerializeField] private EmojiData happyEmojiData;
        [SerializeField] private EmojiData angryEmojiData;

        public void ShowHappyEmojis()
        {
            var emojis = Instantiate(happyEmojiData.emojiPrefab);
            emojis.transform.position = happyEmojiData.startTransform.position;
            emojis.transform.parent = happyEmojiData.startTransform;
        }

        public void ShowAngryEmojis()
        {
            var emojis = Instantiate(angryEmojiData.emojiPrefab);
            emojis.transform.position = angryEmojiData.startTransform.position;
            emojis.transform.parent = angryEmojiData.startTransform;
        }
    }

    [Serializable]
    public class EmojiData
    {
        public Transform startTransform;
        public GameObject emojiPrefab;
    }
}