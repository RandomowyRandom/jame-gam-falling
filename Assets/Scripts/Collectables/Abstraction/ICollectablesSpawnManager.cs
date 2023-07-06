using ServiceLocator;

namespace Collectables.Abstraction
{
    public interface ICollectablesSpawnManager: IService
    {
        public void SetSpawningState(bool shouldSpawn);
    }
}