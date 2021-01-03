using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Levels
{
    public class MainMenuManager : MonoBehaviour
    {
        public void LoadSceneMenu(int menu)
        {
            LevelManager.Instance.LoadLevel(menu);
        }


    }
}