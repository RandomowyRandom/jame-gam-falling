using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth: MonoBehaviour
    {
        public event Action<EnemyHealth> OnEnemyDeath;
        
        public void OnPlayerBulletEnter(GameObject collision)
        {
            var bullet = collision.GetComponent<Player.Shooter.PlayerBullet>();
            
            if(bullet == null)
                return;
            
            OnEnemyDeath?.Invoke(this);
            
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }
}