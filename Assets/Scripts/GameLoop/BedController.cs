using System;
using Collectables;
using GameLoop.Abstraction;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLoop
{
    [RequireComponent(typeof(Animator))]
    public class BedController: MonoBehaviour
    {
        private Animator _animator;
        private IGameLoopManager _gameLoopManager;

        private bool _started;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _gameLoopManager = ServiceLocator.ServiceLocator.Instance.Get<IGameLoopManager>();
        }

        private void Update()
        {
            if(!Keyboard.current.spaceKey.wasPressedThisFrame)
                return;
            
            if (_started)
                return;
            
            _started = true;
            _animator.Play("WakeUp");
        }

        public void OnBedAnimationEnd()
        {
            _animator.Play("Fall");
            
            _gameLoopManager.StartGame();
            Destroy(gameObject, 2f);
        }

        [UsedImplicitly]
        public void PlaySound(AudioClip clip)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}