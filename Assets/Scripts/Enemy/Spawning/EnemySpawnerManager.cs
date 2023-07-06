using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enemy.Abstraction;
using Mono.CSharp;
using UnityEngine;

namespace Enemy.Spawning
{
    public class EnemySpawnerManager: MonoBehaviour, IEnemySpawnerManager
    {
        [SerializeField]
        private List<EnemyHealth> _enemyPrefabs;
        
        [SerializeField]
        private float _spawnInterval = 1f;
        
        [SerializeField]
        private Transform _spawnPoint;
        
        [SerializeField]
        private int _maxEnemies = 3;
        
        private List<EnemyHealth> _spawnedEnemies = new();
        
        private bool _isSpawning = true;

        private void Awake()
        {
            ServiceLocator.ServiceLocator.Instance.Register<IEnemySpawnerManager>(this);
            
            EnemyHealth.OnEnemyDeath += RemoveFromSpawnedEnemies;
        }
        
        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IEnemySpawnerManager>();
            
            EnemyHealth.OnEnemyDeath -= RemoveFromSpawnedEnemies;
        }

        private async void HandleSpawning()
        {
            while (_isSpawning)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval));
                await UniTask.WaitUntil(() => _spawnedEnemies.Count < _maxEnemies);
                
                var randomEnemyIndex = UnityEngine.Random.Range(0, _enemyPrefabs.Count);
                var randomEnemyPrefab = _enemyPrefabs[randomEnemyIndex];
                
                var enemy = Instantiate(randomEnemyPrefab, _spawnPoint.position, Quaternion.identity);
                
                _spawnedEnemies.Add(enemy);
            }
        }

        private void RemoveFromSpawnedEnemies(EnemyHealth enemy)
        {
            _spawnedEnemies.Remove(enemy);
        }

        public void SetSpawningState(bool state)
        {
            switch (state)
            {
                case false:
                    _isSpawning = false;
                    break;
                case true:
                    _isSpawning = true;
                    HandleSpawning();
                    break;
            }
        }
    }
}