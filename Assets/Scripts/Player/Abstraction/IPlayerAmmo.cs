using System;
using ServiceLocator;

namespace Player.Abstraction
{
    public interface IPlayerAmmo: IService
    {
        public event Action<int> OnAmmoChanged;
        
        public int CurrentAmmo { get; }
        
        public void AddAmmo(int ammo = 1);
        
        public void UseAmmo(int ammo = 1);
    }
}