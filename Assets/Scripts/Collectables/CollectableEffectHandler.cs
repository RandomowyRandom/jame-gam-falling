using UnityEngine;

namespace Collectables
{
    public class CollectableEffectHandler: MonoBehaviour
    {
        public void OnCollectableEnter(GameObject collision)
        {
            var collectableBehaviour = collision.GetComponent<CollectableBehaviour>();
            
            if (collectableBehaviour == null)
                return;
            
            var collectableAnimator = collision.GetComponent<Animator>();
            
            if(collectableAnimator != null)
                Destroy(collectableAnimator);
            
            collectableBehaviour.ApplyEffects(this);
        }
    }
}