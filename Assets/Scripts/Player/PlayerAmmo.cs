using System;
using Player.Abstraction;
using UnityEngine;

namespace Player
{
    public class PlayerAmmo: MonoBehaviour, IPlayerAmmo
    {
        public event Action<int> OnAmmoChanged;
        
        public int CurrentAmmo { get; private set; }

        private void Awake()
        {
            ServiceLocator.ServiceLocator.Instance.Register<IPlayerAmmo>(this);
        }

        private void Start()
        {
            CurrentAmmo = 3;
            OnAmmoChanged?.Invoke(CurrentAmmo);
        }

        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IPlayerAmmo>();
        }

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