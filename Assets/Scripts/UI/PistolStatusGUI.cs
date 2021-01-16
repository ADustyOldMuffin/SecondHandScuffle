using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PistolStatusGUI : MonoBehaviour, IWeaponStatusUI
    {
        [SerializeField] private Sprite[] sprites;

        private Image _image;
        
        private void Awake()
        {
            if (EventBus.Instance is null)
                return;
            
            _image = GetComponent<Image>();
            EventBus.Instance.OnWeaponStatusChange += OnWeaponStatusChange;
        }

        private void OnDestroy()
        {
            EventBus.Instance.OnWeaponStatusChange -= OnWeaponStatusChange;
        }

        private void OnWeaponStatusChange(BaseWeapon weapon)
        {
            Debug.Log($"Should fire! {weapon.AmmoCount}");
            _image.sprite = sprites[weapon.AmmoCount];
        }

        public void SetStatus(int value)
        {
            if (value < 0 || value >= sprites.Length)
                return;
            
            _image.sprite = sprites[value];
        }

        public void SetInitialStatus()
        {
            _image.sprite = sprites[sprites.Length - 1];
        }
    }
}