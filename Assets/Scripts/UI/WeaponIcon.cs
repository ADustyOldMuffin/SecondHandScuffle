using Managers;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class WeaponIcon : MonoBehaviour
    {
        [SerializeField] private Image weaponIcon;

        private void Awake()
        {
            if (EventBus.Instance is null)
                return;
        
            EventBus.Instance.OnWeaponChange += OnWeaponChange;
        }

        private void OnDisable()
        {
            if (EventBus.Instance is null)
                return;

            EventBus.Instance.OnWeaponChange -= OnWeaponChange;
        }

        private void OnWeaponChange(BaseWeapon oldWeapon, BaseWeapon newWeapon)
        {
            weaponIcon.sprite = newWeapon.weaponIcon;
        }
    }
}
