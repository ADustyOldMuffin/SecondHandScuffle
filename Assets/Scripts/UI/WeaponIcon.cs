using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

using Managers;

public class WeaponIcon : MonoBehaviour
{
    [SerializeField] Sprite[] weaponIcons;
    [SerializeField] Image UI_Icon;

    // Update is called once per frame
    public void UpdateIcon(int iconIndex)
    {
        UI_Icon.sprite = weaponIcons[iconIndex];
    }
}
