using ServiceLocator;

namespace Player.Abstraction
{
    public interface IPlayerMovement: IService
    {
        public void SetMovementLock(bool isLocked);
    }
}