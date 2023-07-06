using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enemy.Abstraction;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy.Tasks
{
    [Serializable]
    public class LerpToRandomPositionEnemyTask: IEnemyTask
    {
        [SerializeField]
        private List<Vector2> _positions;

        [SerializeField]
        private float _timeToReachPosition = 1f;
        
        [SerializeField]
        private Ease _ease = Ease.Linear;
        
        [Button("Add current position")]
        private void AddCurrentPosition(Transform transform)
        {
            _positions.Add(transform.position);
        }
        
        public async UniTask<EnemyTaskResult> Execute(EnemyBehaviour behaviour)
        {
            if(behaviour == null)
                return EnemyTaskResult.Break;
            
            if(_positions == null || _positions.Count is 0 or 1)
                return EnemyTaskResult.Break;
            
            var randomPosition = GetRandomPosition(behaviour.transform.position);
            var transform = behaviour.transform;

            await transform.DOMove(randomPosition, _timeToReachPosition)
                .SetEase(_ease)
                .AsyncWaitForCompletion();
            
            return EnemyTaskResult.Continue;
        }

        private Vector2 GetRandomPosition(Vector2 currentPosition)
        {
            // get random position from list that is not equal to current position
            var randomPosition = _positions[UnityEngine.Random.Range(0, _positions.Count)];
            while (randomPosition == currentPosition)
            {
                randomPosition = _positions[UnityEngine.Random.Range(0, _positions.Count)];
            }
            
            return randomPosition;
        }
    }
}