using System;
using ServiceLocator;

namespace Player.Abstraction
{
    public interface IPlayerMovement: IService
    {
        public event Action OnPlayerDash;
        public void SetMovementLock(bool isLocked);
    }
}