using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


namespace Managers.Levels
    {
        public class OpeningCinematicManger : MonoBehaviour
        {
            private void Start()
            {
                AudioManager.Instance?.PlayCinematicMusic();
            }
            
            public void LoadSceneMenu(int menu)
            {
                EventBus.Instance?.ChangeLevel(menu);
            }
        }
    }
