using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


namespace Managers.Levels
    {
        public class OpeningCinematicManger : MonoBehaviour
        {
            public void LoadSceneMenu(int menu)
            {
                LevelManager.Instance.LoadLevel(menu);
            }


        }
    }
