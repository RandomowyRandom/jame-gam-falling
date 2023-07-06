using System;
using UnityEngine;

namespace Collectables
{
    [Serializable]
    public class CollectableSpawnData
    {
        [SerializeField]
        private GameObject _collectablePrefab;
        
        [SerializeField] [Range(0f, 1f)]
        private float _spawnChance = 1f;
        
        public GameObject CollectablePrefab => _collectablePrefab;
        
        public float SpawnChance => _spawnChance;
    }
}