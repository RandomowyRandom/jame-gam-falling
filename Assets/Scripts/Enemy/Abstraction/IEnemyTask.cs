using Cysharp.Threading.Tasks;

namespace Enemy.Abstraction
{
    public interface IEnemyTask
    {
        public UniTask<EnemyTaskResult> Execute(EnemyBehaviour behaviour);
    }
}