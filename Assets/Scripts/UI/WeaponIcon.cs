using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

using Managers;

public class WeaponIcon : MonoBehaviour
{
    [SerializeField] Sprite[] weaponIcons;
    [SerializeField] Image _UI_Icon;


    void Awake()
    {
        LevelManager.OnPlayerScoreChange += UpdateIcon;
    }

    void Start()
    {
        UpdateIcon();
    }
    public void UpdateIcon()
    {
        int currentWeapon = LevelManager.Instance.Player.GetComponent<PlayerWeapon>().GetCurrentWeaponIndex();
        Sprite newIcon = weaponIcons[currentWeapon];

        _UI_Icon.sprite = newIcon;
    }
}
