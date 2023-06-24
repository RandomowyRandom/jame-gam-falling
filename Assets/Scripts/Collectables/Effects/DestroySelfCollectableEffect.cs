using System;
using Collectables.Abstraction;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Collectables.Effects
{
    [Serializable]
    public class DestroySelfCollectableEffect: ICollectableEffect
    {
        [SerializeField]
        private Collider2D _collider2D;
        
        public UniTask ApplyEffect(CollectableBehaviour behaviour, CollectableEffectHandler handler)
        {
            _collider2D.enabled = false;
            
            behaviour.transform.DOScale(Vector3.zero, .3f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => Object.Destroy(behaviour.gameObject));
            
            return UniTask.CompletedTask;
        }
    }
}