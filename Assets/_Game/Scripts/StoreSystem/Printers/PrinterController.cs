using System;
using System.Collections.Generic;
using UnityEngine;
using Aezakmi.PrintSystem;
using Aezakmi.StoreSystem.StackSystem;
using DG.Tweening;

namespace Aezakmi.StoreSystem.Printers
{
    public class PrinterController : MonoBehaviour
    {
        public MerchandisePreset merchandisePreset;
        public Material shirtMaterial;
        public Texture shirtTexture;
        public ShirtsZoneData shirtsZoneData;

        [SerializeField] private Transform wingsParent;
        [SerializeField] private List<WingAnimationData> wingAnimationDatas;
        [SerializeField] private float wingsParentMoveDuration;
        [SerializeField] private float wingMoveDuration;
        [SerializeField] private float printingDelay; // Length of delay before the shirt is instantiated and the wings go up.

        private Sequence m_printSequence;
        private List<StackableShirt> m_shirtsToThrow = new List<StackableShirt>();
        private int m_batchCount = 0;

        private void Print()
        {
            for (int i = 0; i < GameManager.Instance.patternsCount; i++)
            {
                var shirt = Instantiate(merchandisePreset.folded);
                shirt.transform.position = wingAnimationDatas[i].shirtSpawn.position;
                shirt.transform.parent = wingAnimationDatas[i].wing;
                shirt.transform.localEulerAngles = wingAnimationDatas[i].shirtLocalEuler;
                shirt.transform.parent = null;
                shirt.GetComponent<Renderer>().material = shirtMaterial;
                shirt.GetComponent<StackableShirt>().parentPrinter = this;
                m_shirtsToThrow.Add(shirt.GetComponent<StackableShirt>());
            }

        }

        private void AnimationComplete()
        {
            m_printSequence.Play();
        }

        public void ActivatePrinter(MerchandisePreset mp, Material sm)
        {
            merchandisePreset = mp;
            shirtMaterial = sm;
            StartPrintingAnimation();
            shirtsZoneData.zone.SetActive(true);
        }

        private void StartPrintingAnimation()
        {
            m_printSequence = DOTween.Sequence();

            Tween rotateWings = wingsParent.DOLocalRotate(Vector3.up * 90f, wingsParentMoveDuration).SetEase(Ease.InOutSine).SetRelative(true);

            Sequence wingsMoveDown = DOTween.Sequence();
            foreach (var wad in wingAnimationDatas)
            {
                Tween wingMoveDown = wad.wing.DOLocalRotate(wad.downEuler, wingMoveDuration).SetEase(Ease.InOutSine);
                wingsMoveDown.Join(wingMoveDown);
            }

            wingsMoveDown.OnComplete(Print);

            Sequence wingsMoveUp = DOTween.Sequence();
            foreach (var wad in wingAnimationDatas)
            {
                Tween wingMoveUp = wad.wing.DOLocalRotate(wad.upEuler, wingMoveDuration).SetEase(Ease.InOutSine);
                wingsMoveUp.Join(wingMoveUp);
            }

            wingsMoveUp.OnComplete(ThrowShirtsToZone);

            m_printSequence
                .Append(rotateWings)
                .Append(wingsMoveDown)
                .AppendInterval(printingDelay)
                .Append(wingsMoveUp)
                .SetLoops(-1, LoopType.Incremental)
                .Play();
        }

        public void ToggleWings()
        {
            foreach (var wad in wingAnimationDatas)
                wad.wing.gameObject.SetActive(false);

            for (int i = 0; i < GameManager.Instance.patternsCount; i++)
                wingAnimationDatas[i].wing.gameObject.SetActive(true);
        }

        private void ThrowShirtsToZone()
        {
            foreach (var shirt in m_shirtsToThrow)
            {
                Sequence throwSequence = DOTween.Sequence();

                Tween moveToPosition = shirt.transform.DOJump(GetPositionInZone(shirt), 1.5f, 1, shirtsZoneData.animationDuration).SetEase(Ease.InOutSine);
                Tween rotate = shirt.transform.DORotate(shirtsZoneData.shirtEuler, shirtsZoneData.animationDuration).SetEase(Ease.InOutSine);

                throwSequence.Append(moveToPosition).Join(rotate).OnComplete(delegate { shirt.collider.enabled = true; }).Play();
                m_batchCount++;
            }

            m_shirtsToThrow = new List<StackableShirt>();
        }

        public void ThrownShirtPickedUp() => m_batchCount = 0;

        private Vector3 GetPositionInZone(StackableShirt shirt)
        {
            var z = (m_batchCount / shirtsZoneData.dimensions.x) % shirtsZoneData.dimensions.y;
            var x = m_batchCount % shirtsZoneData.dimensions.x;
            var y = m_batchCount / (shirtsZoneData.dimensions.x * shirtsZoneData.dimensions.y);

            var targetPosition = shirtsZoneData.firstPosition.position + new Vector3(x * shirt.size.x, y * shirt.size.y, z * shirt.size.z);

            return targetPosition;
        }
    }

    [Serializable]
    public class WingAnimationData
    {
        public Transform wing;
        public Vector3 upEuler;
        public Vector3 downEuler;

        public Transform shirtSpawn;
        public Vector3 shirtLocalEuler;
    }

    [Serializable]
    public class ShirtsZoneData
    {
        public GameObject zone;
        public Transform firstPosition;
        public Vector3 shirtEuler;
        public Vector3Int dimensions;
        public float animationDuration;
    }
}