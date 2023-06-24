using System;
using UnityEngine;

namespace Parallax
{
    public class ParallaxElement: MonoBehaviour
    {
        [SerializeField]
        private float _scrollSpeed = 1f;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public bool IsMoving { get; set; } = true;
        
        private const float START_HEIGHT = 25f;
        
        private void FixedUpdate()
        {
            if(!IsMoving)
                return;
            
            _spriteRenderer.size = new Vector2( _spriteRenderer.size.x, START_HEIGHT + Time.time * _scrollSpeed);
        }
    }
}