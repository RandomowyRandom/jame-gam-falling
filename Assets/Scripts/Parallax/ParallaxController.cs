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

        private void Awake()
        {
            ServiceLocator.ServiceLocator.Instance.Register<IParallaxController>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IParallaxController>();
        }

        public void SetState(bool isMoving)
        {
            _parallaxElements.ForEach(element => 
                element.IsMoving = isMoving);
        }
    }
}