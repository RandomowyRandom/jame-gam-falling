using System;
using System.Collections.Generic;
using System.Linq;
using Collectables.Abstraction;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Collectables
{
    public class CollectablesSpawnManager: MonoBehaviour, ICollectablesSpawnManager
    {
        [SerializeField]
        private List<CollectableSpawnData> _collectablesPrefabs;
        
        [Space(10)]
        [SerializeField]
        private Transform _leftCornerSpawnPoint;
        
        [SerializeField]
        private Transform _rightCornerSpawnPoint;
        
        [Space(10)]
        [SerializeField]
        private float _minSpawnInterval = 1f;
        
        [SerializeField]
        private float _maxSpawnInterval = 3f;

        private bool _shouldSpawnCollectables;
        
        private void Awake()
        {
            ServiceLocator.ServiceLocator.Instance.Register<ICollectablesSpawnManager>(this);
        }

        private void Start()
        {
        }

        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<ICollectablesSpawnManager>();
        }
        
        public void SetSpawningState(bool shouldSpawn)
        {
            _shouldSpawnCollectables = shouldSpawn;
            
            if (_shouldSpawnCollectables)
                HandleSpawnCollectables().Forget();
        }
        
        private async UniTask HandleSpawnCollectables()
        {
            while (_shouldSpawnCollectables)
            {
                var randomCollectableIndex = UnityEngine.Random.Range(0, _collectablesPrefabs.Count);
                var randomCollectablePrefab = GetRandomCollectablePrefab();
                var randomSpawnPointX = UnityEngine.Random.Range(_leftCornerSpawnPoint.position.x, _rightCornerSpawnPoint.position.x);
                var randomSpawnPointY = UnityEngine.Random.Range(_leftCornerSpawnPoint.position.y, _rightCornerSpawnPoint.position.y);
                var randomSpawnPoint = new Vector3(randomSpawnPointX, randomSpawnPointY, 0f);
                
                var collectable = Instantiate(randomCollectablePrefab, randomSpawnPoint, Quaternion.identity);
                collectable.transform.SetParent(transform);
                
                await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(_minSpawnInterval, _maxSpawnInterval)));
            }
        }
        
        private GameObject GetRandomCollectablePrefab()
        {
            var randomValue = UnityEngine.Random.Range(0f, 1f);

            foreach (var collectablePrefab in _collectablesPrefabs.Where(collectablePrefab => randomValue <= collectablePrefab.SpawnChance))
                return collectablePrefab.CollectablePrefab;

            return _collectablesPrefabs.Last().CollectablePrefab;
        }
    }
}