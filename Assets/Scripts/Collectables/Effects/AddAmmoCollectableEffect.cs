using System;
using Collectables.Abstraction;
using Cysharp.Threading.Tasks;
using Player.Abstraction;
using UnityEngine;

namespace Collectables.Effects
{
    [Serializable]
    public class AddAmmoCollectableEffect: ICollectableEffect
    {
        [SerializeField]
        private int _ammoAmount = 1;
        
        private IPlayerAmmo _playerAmmo;
        
        private IPlayerAmmo PlayerAmmo => 
            _playerAmmo ??= ServiceLocator.ServiceLocator.Instance.Get<IPlayerAmmo>();
        public UniTask ApplyEffect(CollectableBehaviour behaviour, CollectableEffectHandler handler)
        {
            PlayerAmmo.AddAmmo(_ammoAmount);
            
            return UniTask.CompletedTask;
        }
    }
}