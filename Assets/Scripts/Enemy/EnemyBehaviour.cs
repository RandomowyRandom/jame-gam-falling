using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enemy.Abstraction;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Enemy
{
    public class EnemyBehaviour: SerializedMonoBehaviour
    {
        [OdinSerialize]
        private List<IEnemyTask> _tasks;
        
        private UniTask<EnemyTaskResult> _currentTask;
        private EnemyTaskResult _lastTaskResult;

        private void Start()
        {
            ExecuteTasks();
        }

        private async void ExecuteTasks()
        {
            if (_tasks == null)
                return;
            
            while (true)
            {
                foreach (var task in _tasks)
                {
                    _currentTask = task.Execute(this);
                    _lastTaskResult = await _currentTask;
                    
                    if (_lastTaskResult == EnemyTaskResult.Break)
                        break;
                }
                
                if (_lastTaskResult == EnemyTaskResult.Break)
                    break;
            }
            
        }
    }
}