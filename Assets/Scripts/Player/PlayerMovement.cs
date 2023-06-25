using System;
using Player.Abstraction;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerMovement: SerializedMonoBehaviour, IPlayerMovement
    {
        [SerializeField]
        private float _fixedYPosition = .7f;

        [SerializeField]
        private float _dashForce = 1f;
        
        [SerializeField]
        private float _movementSpeed = 1f;
        
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        
        private IPlayerStamina _playerStamina;
        
        private Vector2 _movement;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            ServiceLocator.ServiceLocator.Instance.Register<IPlayerMovement>(this);
        }

        private void Start()
        {
            _playerStamina = ServiceLocator.ServiceLocator.Instance.Get<IPlayerStamina>();
        }

        private void Update()
        {
            HandlePlayerRotation();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.AddForce(_movement * _movementSpeed, ForceMode2D.Force);
            
            var isPlayerAtFixedYPosition = Math.Abs(transform.position.y - _fixedYPosition) < .01f;
            
            if (!isPlayerAtFixedYPosition)
                LerpToFixedPosition();
        }

        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IPlayerMovement>();
        }
        
        public void SetMovementLock(bool isLocked)
        {
            enabled = !isLocked;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }
        
        public void OnDash(InputAction.CallbackContext context)
        {
            if (!context.performed) 
                return;
            
            var result = TryDash();
            
            if (result)
                _playerStamina.DrainStamina();
        }

        private void HandlePlayerRotation()
        {
            _spriteRenderer.flipX = _movement.x < 0;
        }
        
        private bool TryDash()
        {
            var hasEnoughStamina = _playerStamina.CurrentStamina > 0;

            if (!hasEnoughStamina)
                return false;
            
            var roundedMovement = new Vector2(Mathf.Round(_movement.x), Mathf.Round(_movement.y));

            if (roundedMovement == Vector2.zero)
                return false;
            
            var dashDirection = roundedMovement.normalized;
            var dashDistance = 1f;
            
            _rigidbody2D.AddForce(dashDirection * dashDistance * _dashForce, ForceMode2D.Impulse);
            return true;
        }

        private void LerpToFixedPosition()
        {
            const float lerpTime = .035f;
            
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, _fixedYPosition, transform.position.z), lerpTime);
        }

    }
}