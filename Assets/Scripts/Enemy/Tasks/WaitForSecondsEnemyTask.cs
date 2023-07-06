using System;
using Cysharp.Threading.Tasks;
using Enemy.Abstraction;
using UnityEngine;

namespace Enemy.Tasks
{
    [Serializable]
    public class WaitForSecondsEnemyTask: IEnemyTask
    {
        [SerializeField]
        private float _timeToWait = 1f;
        
        public async UniTask<EnemyTaskResult> Execute(EnemyBehaviour behaviour)
        {
            if(behaviour == null)
                return EnemyTaskResult.Break;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToWait));
            
            return EnemyTaskResult.Continue;
        }
    }
}