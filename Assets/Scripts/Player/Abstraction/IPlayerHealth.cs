using System;
using ServiceLocator;

namespace Player.Abstraction
{
    public interface IPlayerHealth: IService
    {
        public event Action<int> OnHealthChanged;
        public event Action OnPlayerDeath;
        
        public int CurrentHealth { get; }
        
        public void TakeDamage(int damage = 1);
        
        public void Heal(int health = 1);
    }
}