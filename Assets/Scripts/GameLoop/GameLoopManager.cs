using System;
using System.Collections.Generic;
using Collectables.Abstraction;
using Enemy.Abstraction;
using GameLoop.Abstraction;
using Parallax.Abstraction;
using Player.Abstraction;
using Player.Shooter;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLoop
{
    public class GameLoopManager: SerializedMonoBehaviour, IGameLoopManager
    {
        [SerializeField]
        private SpriteRenderer _playerSpriteRenderer;
        
        [SerializeField]
        private List<GameObject> _objectsToDisableOnStart;
        
        [SerializeField]
        private List<GameObject> _objectsToEnableOnStart;
        
        private IParallaxController _parallaxController;
        private ICollectablesSpawnManager _collectablesSpawnManager;
        private IPlayerMovement _playerMovement;
        private IEnemySpawnerManager _enemySpawnerManager;

        private PlayerShooter _playerShooter;

        private void Awake()
        {
            ServiceLocator.ServiceLocator.Instance.Register<IGameLoopManager>(this);
        }

        private void Start()
        {
            _parallaxController = ServiceLocator.ServiceLocator.Instance.Get<IParallaxController>();
            _collectablesSpawnManager = ServiceLocator.ServiceLocator.Instance.Get<ICollectablesSpawnManager>();
            _playerMovement = ServiceLocator.ServiceLocator.Instance.Get<IPlayerMovement>();
            _enemySpawnerManager = ServiceLocator.ServiceLocator.Instance.Get<IEnemySpawnerManager>();
            _playerShooter = _playerSpriteRenderer.GetComponent<PlayerShooter>();
            
            OnBeforeGameStart();
        }

        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IGameLoopManager>();
        }

        [Button]
        public void OnBeforeGameStart()
        {
            _playerSpriteRenderer.enabled = false;
            _playerShooter.enabled = false;
            
            _parallaxController.SetState(false);
            _collectablesSpawnManager.SetSpawningState(false);
            _playerMovement.SetMovementLock(true);
            _enemySpawnerManager.SetSpawningState(false);
        }
        
        [Button]
        public void StartGame()
        {
            _playerSpriteRenderer.enabled = true;
            _playerShooter.enabled = true;
            
            _parallaxController.SetState(true);
            _collectablesSpawnManager.SetSpawningState(true);
            _playerMovement.SetMovementLock(false);
            _enemySpawnerManager.SetSpawningState(true);
            
            _objectsToDisableOnStart.ForEach(obj => obj.SetActive(false));
            _objectsToEnableOnStart.ForEach(obj => obj.SetActive(true));
        }
        
        [Button]
        public void ResetGame()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}