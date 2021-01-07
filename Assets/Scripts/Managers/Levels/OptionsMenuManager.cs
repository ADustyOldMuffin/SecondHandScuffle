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

            musicVolumeSlider.value = AudioManager.Instance.GetMusicLinearVolume();
            sfxVolumeSlider.value = AudioManager.Instance.GetSfxLinearVolume();
        }

        public void OnMusicVolumeChanged()
        {
            AudioManager.Instance.SetMusicVolume(musicVolumeSlider.value);
        }

        public void OnSfxVolumeChanged()
        {
            AudioManager.Instance.SetSfxVolume(musicVolumeSlider.value);
        }

        public void OnBackButtonPressed()
        {
            LevelManager.Instance.ChangeLevel(Level.MainMenu);
        }
    }
}