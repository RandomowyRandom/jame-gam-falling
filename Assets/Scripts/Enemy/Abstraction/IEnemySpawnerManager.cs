using ServiceLocator;

namespace Enemy.Abstraction
{
    public interface IEnemySpawnerManager: IService
    {
        public void SetSpawningState(bool state);
    }
}