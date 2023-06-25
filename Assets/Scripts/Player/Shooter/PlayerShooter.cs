using System;
using Player.Abstraction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Shooter
{
    public class PlayerShooter: MonoBehaviour
    {
        [SerializeField]
        private PlayerBullet _bulletPrefab;

        [SerializeField]
        private float _force;
        
        private IPlayerAmmo _playerAmmo;

        private void Start()
        {
            _playerAmmo = ServiceLocator.ServiceLocator.Instance.Get<IPlayerAmmo>();
        }

        private void Update()
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
                TryShoot();
        }

        private void TryShoot()
        {
            if (_playerAmmo.CurrentAmmo <= 0)
            {
                return;
            }
            
            _playerAmmo.UseAmmo();
            var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            
            bulletRigidbody.AddForce(GetShootDirection() * _force, ForceMode2D.Impulse);
        }
        
        private Vector2 GetShootDirection()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var direction = (Vector2) worldMousePosition - (Vector2) transform.position;
            direction.Normalize();
            return direction;
        }
    }
}