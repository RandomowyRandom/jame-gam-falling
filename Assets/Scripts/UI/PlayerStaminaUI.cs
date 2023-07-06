using System.Collections.Generic;
using Player.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerStaminaUI: MonoBehaviour
    {
        [SerializeField] 
        private List<Image> _staminaIndicator;
        
        [SerializeField]
        private Sprite _fullStaminaSprite;
        
        [SerializeField]
        private Sprite _emptyStaminaSprite;

        private IPlayerStamina _playerStamina;
        
        private void Start()
        {
            _playerStamina = ServiceLocator.ServiceLocator.Instance.Get<IPlayerStamina>();
            _playerStamina.OnStaminaChanged += UpdateUI;
        }

        private void OnDestroy()
        {
            _playerStamina.OnStaminaChanged -= UpdateUI;
        }

        private void UpdateUI(int amount)
        {
            SetIndicators(amount);
        }

        private void SetIndicators(int amount)
        {
            for (var i = 0; i < _staminaIndicator.Count; i++)
            {
                _staminaIndicator[i].sprite = i < amount ? _fullStaminaSprite : _emptyStaminaSprite;
            }
        }
    }
}