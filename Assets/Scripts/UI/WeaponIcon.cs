using System;
using UnityEngine;

using Managers;
using Weapons;
using Image = UnityEngine.UIElements.Image;

public class WeaponIcon : MonoBehaviour
{
    [SerializeField] Sprite[] weaponIcons;
    [SerializeField] GameObject _weaponIcon;


    private void Awake()
    {
        LevelManager.OnPlayerWeaponChange += OnWeaponChangedUpdate;
    }

    private void OnDestroy()
    {
        LevelManager.OnPlayerWeaponChange -= OnWeaponChangedUpdate;
    }

    private void OnWeaponChangedUpdate(BaseWeapon weapon)
    {
        _weaponIcon.GetComponent<UnityEngine.UI.Image>().sprite = weaponIcons[(int)weapon.type];
    }
}
