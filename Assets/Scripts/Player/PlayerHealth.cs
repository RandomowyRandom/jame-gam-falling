using System;
using JetBrains.Annotations;
using Player.Abstraction;
using QFSW.QC;
using UnityEngine;

namespace Player
{
    public class PlayerHealth: MonoBehaviour, IPlayerHealth
    {
        public event Action<int> OnHealthChanged;

        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = 3;
            ServiceLocator.ServiceLocator.Instance.Register<IPlayerHealth>(this);
        }
        
        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IPlayerHealth>();
        }

        public void TakeDamage(int damage = 1)
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, 3);
            
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public void Heal(int health = 1)
        {
            CurrentHealth += health;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, 3);
            
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        #region QC

        [Command("take-damage")] [UsedImplicitly]
        private void CommandTakeDamage(int damage = 1)
        {
            TakeDamage(damage);
        }
        
        [Command("heal")] [UsedImplicitly]
        private void CommandHeal(int health = 1)
        {
            Heal(health);
        }
        
        #endregion
    }
}