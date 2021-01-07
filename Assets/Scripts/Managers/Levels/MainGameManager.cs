using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Managers.Levels
{
    public class MainGameManager : MonoBehaviour
    {
        EnemySpawner[] enemySpawners;


        //pause menu
        public static bool gameIsPaused = false;
        public GameObject PauseMenuUI;

        //volume sliders
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        private void Awake()
        {
            if (LevelManager.Instance == null)
                return;
            
            LevelManager.Instance.SetPlayerScore(0);
            LevelManager.OnPlayerDeath += PlayerDied;
            //can't get this to work. wip. learning input system. I did add P and space as pause options.
            //InputManager.Instance.InputMaster.UI.Pause.performed += OnPauseKey;
            enemySpawners = FindObjectsOfType<EnemySpawner>();

            if (AudioManager.Instance == null)
                return;

            musicVolumeSlider.value = AudioManager.Instance.GetMusicLinearVolume();
            sfxVolumeSlider.value = AudioManager.Instance.GetSfxLinearVolume();
        }

        private void Start()
        {
            if (AudioManager.Instance == null)
                return;

            StartCoroutine(AudioManager.Instance.PlayBattleTheme());
        }

        


        private void OnDestroy()
        {
            LevelManager.OnPlayerDeath -= PlayerDied;
        }

        private void PlayerDied()
        {
            StopSpawners();
            LevelManager.Instance.LoadLevel((int)Level.GameOver);
        }

        private void StopSpawners()
        {
            foreach (var t in enemySpawners)
            {
                t.StopSpawning();
            }
        }


        /***
         * related to pause menu
         ***/


        //need to set up keybinding for pause menu. P and Space?
        private void OnPauseKey()
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        private void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
        private void Pause()
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        //necessary for to halt spawners, player movement, etc
        //not currenlty implemented
        public bool IsGamePaused()
        {
            return gameIsPaused;
        }

        public void ClosePauseMenu()
        {
            Resume();
        }

        //used for restart and main menu buttons
        public void LoadScene(int level)
        {
            LevelManager.Instance.LoadLevel(level);
        }

        public void OnMusicVolumeChanged()
        {
            AudioManager.Instance.SetMusicVolume(musicVolumeSlider.value);
        }

        public void OnSfxVolumeChanged()
        {
            AudioManager.Instance.SetSfxVolume(musicVolumeSlider.value);
        }
    }
}