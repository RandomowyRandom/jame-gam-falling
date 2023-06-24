using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement: SerializedMonoBehaviour
    {
        [SerializeField]
        private float _fixedYPosition = .7f;

        [SerializeField]
        private float _dashForce = 1f;
        
        [SerializeField]
        private float _movementSpeed = 1f;
        
        private Rigidbody2D _rigidbody2D;
        
        private Vector2 _movement;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.AddForce(_movement * _movementSpeed, ForceMode2D.Force);
            
            var isPlayerAtFixedYPosition = Math.Abs(transform.position.y - _fixedYPosition) < .01f;
            
            if (!isPlayerAtFixedYPosition)
                LerpToFixedPosition();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }
        
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                Dash();
        }

        private void Dash()
        {
            // read the movement input and dash in that direction, rounded to the nearest 45 degrees
            var roundedMovement = new Vector2(Mathf.Round(_movement.x), Mathf.Round(_movement.y));
            
            if (roundedMovement == Vector2.zero)
                return;
            
            var dashDirection = roundedMovement.normalized;
            var dashDistance = 1f;
            
            _rigidbody2D.AddForce(dashDirection * dashDistance * _dashForce, ForceMode2D.Impulse);
        }

        private void LerpToFixedPosition()
        {
            const float lerpTime = .035f;
            
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, _fixedYPosition, transform.position.z), lerpTime);
        }
    }
}