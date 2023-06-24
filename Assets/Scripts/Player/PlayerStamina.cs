using System;
using JetBrains.Annotations;
using Player.Abstraction;
using QFSW.QC;
using UnityEngine;

namespace Player
{
    public class PlayerStamina: MonoBehaviour, IPlayerStamina
    {
        public event Action<int> OnStaminaChanged;
        
        public int CurrentStamina { get; private set; }

        private void Awake()
        {
            CurrentStamina = 3;
            ServiceLocator.ServiceLocator.Instance.Register<IPlayerStamina>(this);
        }
        
        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IPlayerStamina>();
        }

        public void DrainStamina(int stamina = 1)
        {
            CurrentStamina -= stamina;
            CurrentStamina = Mathf.Clamp(CurrentStamina, 0, 3);
            
            OnStaminaChanged?.Invoke(CurrentStamina);
        }

        public void RestoreStamina(int stamina = 1)
        {
            CurrentStamina += stamina;
            CurrentStamina = Mathf.Clamp(CurrentStamina, 0, 3);
            
            OnStaminaChanged?.Invoke(CurrentStamina);
        }
        
        #region QC

        [Command("drain-stamina")] [UsedImplicitly]
        private void CommandDrainStamina(int amount = 1)
        {
            DrainStamina(amount);
        }
        
        [Command("restore-stamina")] [UsedImplicitly]
        private void CommandRestoreStamina(int amount = 1)
        {
            RestoreStamina(amount);
        }

        #endregion
    }
}