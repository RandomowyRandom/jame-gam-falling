using System;
using UnityEngine;

namespace Parallax
{
    public class ParallaxElement: MonoBehaviour
    {
        [SerializeField]
        private float _maxY = 10f;
        
        [SerializeField]
        private float _minY = -10f;
        
        [SerializeField]
        private float _speed = 1f;
        
        public bool IsMoving { get; set; }

        private void FixedUpdate()
        {
            if(!IsMoving)
                return;
            
            transform.position += Vector3.up * (_speed * Time.fixedDeltaTime);
            
            if (transform.position.y > _maxY)
                transform.position = new Vector3(transform.position.x, _minY, transform.position.z);
        }
    }
}