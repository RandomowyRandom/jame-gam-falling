using System;
using ServiceLocator;

namespace Player.Abstraction
{
    public interface IPlayerStamina: IService
    {
        public event Action<int> OnStaminaChanged;
        
        public int CurrentStamina { get; }
        
        public void DrainStamina(int stamina = 1);
        
        public void RestoreStamina(int stamina = 1);
    }
}