using System;
using System.Collections.Generic;
using UnityEngine;
using NativeSerializableDictionary;

namespace Aezakmi.StoreSystem
{
    public class StoreSizeManager : GloballyAccessibleBase<StoreSizeManager>
    {
        [SerializeField] private List<GameObject> fullyReplaceableGameobjectsPerStoreSizeLevel;
        [SerializeField] private SerializableDictionary<int, List<Repositioner>> repositionersPerStoreSizeLevel;
        [SerializeField] private SerializableDictionary<int, List<GameObject>> objectsAddedPerStoreSizeLevel;

        public void UpgradeStoreSize(int level)
        {
            if (level == 0) return;

            fullyReplaceableGameobjectsPerStoreSizeLevel[level - 1].SetActive(false);
            fullyReplaceableGameobjectsPerStoreSizeLevel[level].SetActive(true);

            foreach (var rep in repositionersPerStoreSizeLevel[level].Value)
                rep.t.position = rep.position;

            foreach (var obj in objectsAddedPerStoreSizeLevel[level].Value)
                obj.SetActive(true);

            Physics.SyncTransforms();
            AstarPath.active.Scan();
        }
    }

    [Serializable]
    public class Repositioner
    {
        public Transform t;
        public Vector3 position;
    }
}