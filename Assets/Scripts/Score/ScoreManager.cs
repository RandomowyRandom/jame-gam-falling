using System;
using Enemy;
using Mono.CSharp;
using Score.Abstraction;
using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreManager: MonoBehaviour, IScoreManager
    {
        [SerializeField]
        private TMP_Text _scoreText;
        
        public event Action<int> OnScoreChanged;
        public int Score { get; private set; }

        private void Awake()
        {
            ServiceLocator.ServiceLocator.Instance.Register<IScoreManager>(this);
        }
        
        private void OnDestroy()
        {
            ServiceLocator.ServiceLocator.Instance.Deregister<IScoreManager>();
            
            EnemyHealth.OnEnemyDeath -= HandleScore;
            OnScoreChanged -= UpdateScoreText;
        }

        private void Start()
        {
            Score = 0;
            
            EnemyHealth.OnEnemyDeath += HandleScore;
            OnScoreChanged += UpdateScoreText;
            
            OnScoreChanged?.Invoke(Score);
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.SetText(score.ToString());
        }

        private void HandleScore(EnemyHealth enemy)
        {
            Score++;

            OnScoreChanged?.Invoke(Score);
        }

    }
}