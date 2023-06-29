using UnityEngine;
using Aezakmi.StoreSystem.Printers;

namespace Aezakmi.StoreSystem.StackSystem
{
    [RequireComponent(typeof(Collider))]
    public class StackableShirt : MonoBehaviour
    {
        public PrinterController parentPrinter;
        public Vector3 size;
        public bool animated = false;
        public new Collider collider;
    }
}