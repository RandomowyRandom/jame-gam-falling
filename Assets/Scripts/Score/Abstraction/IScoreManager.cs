using System;
using ServiceLocator;

namespace Score.Abstraction
{
    public interface IScoreManager: IService
    {
        public event Action<int> OnScoreChanged;
        
        public int Score { get; }
    }
}