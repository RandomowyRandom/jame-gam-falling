using System;
using Collectables.Abstraction;
using Cysharp.Threading.Tasks;
using Player.Abstraction;
using UnityEngine;

namespace Collectables.Effects
{
    [Serializable]
    public class HealCollectableEffect: ICollectableEffect
    {
        [SerializeField]
        private int _healAmount;
        
        private IPlayerHealth _playerHealth;
        
        private IPlayerHealth PlayerHealth => 
            _playerHealth ??= ServiceLocator.ServiceLocator.Instance.Get<IPlayerHealth>();
        
        public UniTask ApplyEffect(CollectableBehaviour behaviour, CollectableEffectHandler handler)
        {
            PlayerHealth.Heal(_healAmount);
            
            return UniTask.CompletedTask;
        }
    }
}