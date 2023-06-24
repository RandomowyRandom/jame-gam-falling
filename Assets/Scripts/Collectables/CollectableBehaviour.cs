using System.Collections.Generic;
using Collectables.Abstraction;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Collectables
{
    public class CollectableBehaviour: SerializedMonoBehaviour
    {
        [OdinSerialize]
        private List<ICollectableEffect> _effects;
        
        public void ApplyEffects(CollectableEffectHandler handler)
        {
            if(_effects == null)
                return;
            
            foreach (var effect in _effects)
                effect.ApplyEffect(this, handler);
        }
    }
}