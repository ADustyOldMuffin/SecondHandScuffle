using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Levels
{
    public class MainMenuManager : MonoBehaviour
    {
        private void Start()
        {
            if (AudioManager.Instance == null)
                return;
            
            AudioManager.Instance.PlayMainMenuMusic();
        }

        public void LoadSceneMenu(int menu)
        {
            LevelManager.Instance.LoadLevel(menu);
        }
    }
}