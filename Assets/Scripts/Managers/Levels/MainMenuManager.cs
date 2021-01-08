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
            AudioManager.Instance?.PlayMainMenuMusic();
        }
    }
}