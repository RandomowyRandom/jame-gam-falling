using System;
using Cysharp.Threading.Tasks;
using Enemy.Abstraction;
using Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Enemy.Tasks
{
    [Serializable]
    public class ShootToPlayerEnemyTask: IEnemyTask
    {
        [SerializeField]
        private Rigidbody2D _bulletPrefab;
        
        [SerializeField]
        private float _bulletForce = 5f;
        
        private Transform _playerTransform;

        private Transform PlayerTransform => _playerTransform ??= Object.FindObjectOfType<PlayerMovement>().transform;
        public UniTask<EnemyTaskResult> Execute(EnemyBehaviour behaviour)
        {
            if(behaviour == null)
                return UniTask.FromResult(EnemyTaskResult.Break);
            
            var direction = (PlayerTransform.position - behaviour.transform.position).normalized;
            
            var bullet = Object.Instantiate(_bulletPrefab, behaviour.transform.position, Quaternion.identity);
            bullet.AddForce(direction * _bulletForce, ForceMode2D.Impulse);
            
            return UniTask.FromResult(EnemyTaskResult.Continue);
        }
    }
}