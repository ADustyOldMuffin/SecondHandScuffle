using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Managers
{
    public class AudioManager : SingletonBehavior<AudioManager>
    {
        [SerializeField] private AudioSource musicPlayer;
        [SerializeField] private AudioClip cinematicMusic;
        [SerializeField] private AudioClip mainMenuMusic;
        [SerializeField] private AudioClip battleIntroMusic;
        [SerializeField] private AudioClip battleMusic;
        [SerializeField] private AudioMixer mixer;

        private MusicClip _currentSong;

        private void Start()
        {
            // This isn't working locally as it seems to set the floats to random values
            // For now until a fix can be found/confirmed it's only in the editor it's better just just default them
            PlayerPrefs.DeleteAll();
            
            if(!PlayerPrefs.HasKey("SFXVolume"))
                PlayerPrefs.SetFloat("SFXVolume", 1.0f);
            
            if(!PlayerPrefs.HasKey("MusicVolume"))
                PlayerPrefs.SetFloat("MusicVolume", 1.0f);

            musicPlayer.pitch = .5f;

            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1.0f));
            SetSfxVolume(PlayerPrefs.GetFloat("SFXVolume", 1.0f));
        }

        public void SetMusicVolume(float value)
        {
            PlayerPrefs.SetFloat("MusicVolume", LinearToDecibels(value));
            mixer.SetFloat("MusicVolume", LinearToDecibels(value));
            
            PlayerPrefs.Save();
        }

        public void SetSfxVolume(float value)
        {
            PlayerPrefs.SetFloat("SFXVolume", LinearToDecibels(value));
            mixer.SetFloat("SFXVolume", LinearToDecibels(value));
            
            PlayerPrefs.Save();
        }

        public float GetMusicLinearVolume()
        {
            return DecibelsToLinear(PlayerPrefs.GetFloat("MusicVolume"));
        }

        public float GetSfxLinearVolume()
        {
            return DecibelsToLinear(PlayerPrefs.GetFloat("SFXVolume"));
        }

        public void PlayCinematicMusic()
        {
            if (_currentSong == MusicClip.OpeningTheme)
                return;

            _currentSong = MusicClip.OpeningTheme;
            musicPlayer.clip = cinematicMusic;
            musicPlayer.loop = true;
            musicPlayer.Play();
        }

        public void PlayMainMenuMusic()
        {
            if (_currentSong == MusicClip.MainMenuTheme)
                return;
            
            _currentSong = MusicClip.MainMenuTheme;
            musicPlayer.clip = mainMenuMusic;
            musicPlayer.loop = true;
            musicPlayer.Play();
        }

        public IEnumerator PlayBattleTheme()
        {
            if (_currentSong == MusicClip.BattleTheme)
                yield break;
            
            _currentSong = MusicClip.BattleTheme;
            musicPlayer.clip = battleIntroMusic;
            musicPlayer.loop = false;
            musicPlayer.Play();

            while (musicPlayer.isPlaying)
                yield return null;

            musicPlayer.clip = battleMusic;
            musicPlayer.loop = true;
            musicPlayer.Play();
        }
        
        private static float LinearToDecibels(float value)
        {
            return Mathf.Log10(value) * 20;
        }

        private static float DecibelsToLinear(float value)
        {
            return Mathf.Pow(10.0f, value / 20.0f);
        }
    }
}