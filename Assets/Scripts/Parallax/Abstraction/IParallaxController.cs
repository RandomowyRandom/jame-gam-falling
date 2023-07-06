using System.Collections.Generic;
using ServiceLocator;
using UnityEngine;

namespace Parallax.Abstraction
{
    public interface IParallaxController: IService
    {
        public void SetState(bool isMoving);
    }
}