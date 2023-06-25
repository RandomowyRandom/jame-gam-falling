using UnityEngine;

namespace Enemy
{
    public class EnemyHealth: MonoBehaviour
    {
        public void OnPlayerBulletEnter(GameObject collision)
        {
            var bullet = collision.GetComponent<Player.Shooter.PlayerBullet>();
            
            if(bullet == null)
                return;
            
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }
}