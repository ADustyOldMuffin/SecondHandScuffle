using System;
using Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.Levels
{
    public class OptionsMenuManager : MonoBehaviour
    {
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        private void Start()
        {
            if (AudioManager.Instance == null)
                return; 
            musicVolumeSlider.value = AudioManager.GetMusicLinearVolume();
            sfxVolumeSlider.value = AudioManager.GetSfxLinearVolume();
        }

        public void OnMusicVolumeChanged()
        {
            AudioManager.Instance?.SetMusicVolume(musicVolumeSlider.value);
        }

        public void OnSfxVolumeChanged()
        {
            AudioManager.Instance?.SetSfxVolume(musicVolumeSlider.value);
        }
    }
}