using System;
using DG.Tweening;
using Player.Abstraction;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerEffects: MonoBehaviour
    {
        [SerializeField]
        private AudioClip _dashSound;
        
        [SerializeField]
        private AudioClip _damageSound;
        
        private SpriteRenderer _spriteRenderer;

        private IPlayerHealth _playerHealth;
        private IPlayerMovement _playerMovement;
        
        private readonly int _hitEffectBlend = Shader.PropertyToID("_HitEffectBlend");
        private readonly int _hitEffectColor = Shader.PropertyToID("_HitEffectColor");

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _playerHealth = ServiceLocator.ServiceLocator.Instance.Get<IPlayerHealth>();
            _playerMovement = ServiceLocator.ServiceLocator.Instance.Get<IPlayerMovement>();
            
            _playerMovement.OnPlayerDash += HandlePlayerDash;
            _playerHealth.OnHealthChanged += TryHandlePlayerDamage;
        }

        private void HandlePlayerDash()
        {
            _spriteRenderer.material.SetColor(_hitEffectColor, Color.green);
            _spriteRenderer.material.SetFloat(_hitEffectBlend, 1);
            _spriteRenderer.material.DOFloat(0f, _hitEffectBlend, 0.5f).SetEase(Ease.InQuart);
            
            AudioSource.PlayClipAtPoint(_dashSound, transform.position);
        }

        private void TryHandlePlayerDamage(int amount, PlayerHealthChangeType type)
        {
            if(!type.Equals(PlayerHealthChangeType.Damage))
                return;
            
            _spriteRenderer.material.SetColor(_hitEffectColor, Color.red);
            _spriteRenderer.material.SetFloat(_hitEffectBlend, 1);
            _spriteRenderer.material.DOFloat(0f, _hitEffectBlend, 0.5f).SetEase(Ease.InQuart);
            
            AudioSource.PlayClipAtPoint(_damageSound, transform.position);
        }
    }
}