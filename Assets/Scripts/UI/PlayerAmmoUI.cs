using System;
using System.Collections.Generic;
using Player.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerAmmoUI: MonoBehaviour
    {
        [SerializeField] 
        private List<Image> _ammoIndicator;
        
        [SerializeField]
        private Sprite _fullAmmoSprite;
        
        [SerializeField]
        private Sprite _emptyAmmoSprite;
        
        private IPlayerAmmo _playerAmmo;

        private void Start()
        {
            _playerAmmo = ServiceLocator.ServiceLocator.Instance.Get<IPlayerAmmo>();
            _playerAmmo.OnAmmoChanged += UpdateUI;
        }

        private void OnDestroy()
        {
            _playerAmmo.OnAmmoChanged -= UpdateUI;
        }
        
        private void UpdateUI(int amount)
        {
            SetIndicators(amount);
        }
        
        private void SetIndicators(int amount)
        {
            for (var i = 0; i < _ammoIndicator.Count; i++)
            {
                _ammoIndicator[i].sprite = i < amount ? _fullAmmoSprite : _emptyAmmoSprite;
            }
        }
    }
}