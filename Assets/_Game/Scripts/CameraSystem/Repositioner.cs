using System;
using UnityEngine;
using DG.Tweening;

namespace Aezakmi.CameraSystem
{
    [Serializable]
    public class Repositioner
    {
        public Vector3 position;
        public Vector3 rotation;
        public float duration;
        public Ease ease;
    }
}
