using System;
using Collectables.Abstraction;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Collectables.Effects
{
    [Serializable]
    public class PlaySoundCollectableEffect: ICollectableEffect
    {
        [SerializeField]
        private AudioClip _sound;
        public UniTask ApplyEffect(CollectableBehaviour behaviour, CollectableEffectHandler handler)
        {
            AudioSource.PlayClipAtPoint(_sound, behaviour.transform.position);
            
            return UniTask.CompletedTask;
        }
    }
}