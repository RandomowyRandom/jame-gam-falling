using System;
using Collectables.Abstraction;
using Cysharp.Threading.Tasks;
using Player.Abstraction;
using UnityEngine;

namespace Collectables.Effects
{
    [Serializable]
    public class RestoreStaminaCollectableEffect: ICollectableEffect
    {
        [SerializeField]
        private int _staminaAmount;
        
        private IPlayerStamina _playerStamina;
        
        private IPlayerStamina PlayerStamina => 
            _playerStamina ??= ServiceLocator.ServiceLocator.Instance.Get<IPlayerStamina>();
        
        public UniTask ApplyEffect(CollectableBehaviour behaviour, CollectableEffectHandler handler)
        {
            PlayerStamina.RestoreStamina(_staminaAmount);
            
            return UniTask.CompletedTask;
        }
    }
}