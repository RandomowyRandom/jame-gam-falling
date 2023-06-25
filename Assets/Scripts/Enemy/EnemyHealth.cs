using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth: MonoBehaviour
    {
        [SerializeField]
        private GameObject _deathParticles;
        
        public static event Action<EnemyHealth> OnEnemyDeath;
        
        public void OnPlayerBulletEnter(GameObject collision)
        {
            var bullet = collision.GetComponent<Player.Shooter.PlayerBullet>();
            
            if(bullet == null)
                return;
            
            OnEnemyDeath?.Invoke(this);
            
            Instantiate(_deathParticles, transform.position, Quaternion.identity);
            
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }
}