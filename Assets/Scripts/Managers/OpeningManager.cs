using System;
using UnityEngine;

namespace Managers
{
    public class OpeningManager : MonoBehaviour
    {
        private void Start()
        {
            if (AudioManager.Instance == null)
                return;
            
            AudioManager.Instance.PlayCinematicMusic();
        }
    }
}