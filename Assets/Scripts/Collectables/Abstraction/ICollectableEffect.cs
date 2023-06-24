using Cysharp.Threading.Tasks;

namespace Collectables.Abstraction
{
    public interface ICollectableEffect
    {
        public UniTask ApplyEffect(CollectableBehaviour behaviour, CollectableEffectHandler handler);
    }
}