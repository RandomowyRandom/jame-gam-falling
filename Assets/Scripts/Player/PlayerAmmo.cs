using System;
using Player.Abstraction;
using UnityEngine;

namespace Player
{
    public class PlayerAmmo: MonoBehaviour, IPlayerAmmo
    {
        public event Action<int> OnAmmoChanged;
        
        public int CurrentAmmo { get; private set; }
        
        public void AddAmmo(int ammo = 1)
        {
            CurrentAmmo += ammo;
            CurrentAmmo = Mathf.Clamp(CurrentAmmo, 0, 3);
            
            OnAmmoChanged?.Invoke(CurrentAmmo);
        }

        public void UseAmmo(int ammo = 1)
        {
            CurrentAmmo -= ammo;
            CurrentAmmo = Mathf.Clamp(CurrentAmmo, 0, 3);
            
            OnAmmoChanged?.Invoke(CurrentAmmo);
        }
    }
}