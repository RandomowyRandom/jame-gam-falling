using ServiceLocator;

namespace GameLoop.Abstraction
{
    public interface IGameLoopManager: IService
    {
        public void OnBeforeGameStart();
        public void StartGame();
        
        public void ResetGame();
    }
}