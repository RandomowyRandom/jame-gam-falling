using System;
using System.Collections.Generic;
using Player.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHealthUI: MonoBehaviour
    {
        [SerializeField] 
        private List<Image> _healthIndicator;
        
        [SerializeField]
        private Sprite _fullHealthSprite;
        
        [SerializeField]
        private Sprite _emptyHealthSprite;

        private IPlayerHealth _playerHealth;
        
        private void Start()
        {
            _playerHealth = ServiceLocator.ServiceLocator.Instance.Get<IPlayerHealth>();
            _playerHealth.OnHealthChanged += UpdateUI;
        }

        private void OnDestroy()
        {
            _playerHealth.OnHealthChanged -= UpdateUI;
        }

        private void UpdateUI(int amount)
        {
            SetIndicators(amount);
        }

        private void SetIndicators(int amount)
        {
            for (var i = 0; i < _healthIndicator.Count; i++)
            {
                _healthIndicator[i].sprite = i < amount ? _fullHealthSprite : _emptyHealthSprite;
            }
        }
    }
}