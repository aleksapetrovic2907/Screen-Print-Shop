#pragma warning disable 618

using System.Collections.Generic;
using UnityEngine;
using Aezakmi.StoreSystem;
using Aezakmi.Tweens;
using DG.Tweening;
using NativeSerializableDictionary;

namespace Aezakmi.Zones
{
    public class StoreCounterZone : ZoneBase
    {
        [SerializeField] private StoreCounter storeCounter;
        [SerializeField] private Scale vfxScale;
        [SerializeField] private SerializableDictionary<ParticleSystem, Color> colorByParticlesUnsupervised;
        [SerializeField] private SerializableDictionary<ParticleSystem, Color> colorByParticlesSupervised;

        protected override void OnZoneLoaded()
        {
            storeCounter.isSupervised = true;

            vfxScale.Tweener.SmoothRewind();

            foreach (var cbp in colorByParticlesSupervised)
            {
                cbp.Key.startColor = cbp.Value.Value;
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            storeCounter.isSupervised = false;
            vfxScale.PlayTween();

            foreach (var cbp in colorByParticlesUnsupervised)
            {
                cbp.Key.startColor = cbp.Value.Value;
            }
        }
    }
}