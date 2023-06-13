using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.StoreSystem
{
    public class StoreAccesswaysManager : GloballyAccessibleBase<StoreAccesswaysManager>
    {
        [SerializeField] private List<Transform> entrances;
        [SerializeField] private List<Transform> exits;

        public Vector3 GetRandomEntrance() => entrances[Random.Range(0, entrances.Count)].position;
        public Vector3 GetRandomExit() => exits[Random.Range(0, exits.Count)].position;
    }
}