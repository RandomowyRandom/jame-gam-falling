using System;
using System.Collections.Generic;
using Collectables.Abstraction;
using DG.Tweening;
using Enemy.Abstraction;
using GameLoop.Abstraction;
using Parallax.Abstraction;
using Player.Abstraction;
using Player.Shooter;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
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
        
        [SerializeField]
        private List<GameObject> _objectsToEnableOnStop;
        
        [SerializeField]
        private List<GameObject> _objectsToDisableOnStop;
        
        [SerializeField]
        private VolumeProfile _stopGameVolume;
        
        [SerializeField]
        private Volume _globalVolume;
        
        private IParallaxController _parallaxController;
        private ICollectablesSpawnManager _collectablesSpawnManager;
        private IPlayerMovement _playerMovement;
        private IEnemySpawnerManager _enemySpawnerManager;
        private IPlayerHealth _playerHealth;

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
            _playerHealth = ServiceLocator.ServiceLocator.Instance.Get<IPlayerHealth>();
            _playerShooter = _playerSpriteRenderer.GetComponent<PlayerShooter>();
            
            _playerHealth.OnPlayerDeath += StopGame;
            
            OnBeforeGameStart();
        }

        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IGameLoopManager>();
            
            _playerHealth.OnPlayerDeath -= StopGame;
        }

        [Button]
        public void OnBeforeGameStart()
        {
            Time.timeScale = 1;
            
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
            ServiceLocator.ServiceLocator.Instance.DeregisterAll();
            DOTween.KillAll();
            
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        [Button]
        public void StopGame()
        {
            _collectablesSpawnManager.SetSpawningState(false);
            _playerMovement.SetMovementLock(true);
            _enemySpawnerManager.SetSpawningState(false);

            _objectsToDisableOnStop.ForEach(obj => obj.SetActive(false));
            _objectsToEnableOnStop.ForEach(obj => obj.SetActive(true));
            
            _globalVolume.profile = _stopGameVolume;
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, .3f);
        }
    }
}