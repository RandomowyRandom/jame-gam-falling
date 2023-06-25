using System;
using System.Collections.Generic;
using Parallax.Abstraction;
using UnityEngine;

namespace Parallax
{
    public class ParallaxController: MonoBehaviour, IParallaxController
    {
        [SerializeField]
        private List<ParallaxElement> _parallaxElements;
        
        [SerializeField]
        private ParticleSystem _fallingParticles;

        private void Awake()
        {
            ServiceLocator.ServiceLocator.Instance.Register<IParallaxController>(this);
            SetState(false);
        }

        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IParallaxController>();
        }

        public void SetState(bool isMoving)
        {
            _parallaxElements.ForEach(element => 
                element.IsMoving = isMoving);
            
            if (isMoving)
                _fallingParticles.Play();
            else
                _fallingParticles.Stop();
        }
    }
}