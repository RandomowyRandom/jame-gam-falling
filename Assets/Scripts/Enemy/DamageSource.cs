using UnityEngine;

namespace Enemy
{
    public class DamageSource: MonoBehaviour
    {
        [SerializeField]
        private bool _destroyOnCollision = true;

        [field: SerializeField] 
        public float Damage { get; set; } = 1;
        
        public bool DestroyOnCollision => _destroyOnCollision;
    }
}