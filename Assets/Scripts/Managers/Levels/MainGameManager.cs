using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using Lean.Gui;
using Lean.Transition;
using Lean.Transition.Extras;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers.Levels
{
    public class MainGameManager : MonoBehaviour
    {
        EnemySpawner[] enemySpawners;


        //pause menu
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;

        //volume sliders
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        
        [Header("Lean Animations")]
        [SerializeField] private Transform highlightCircle;
        [SerializeField] private CanvasGroup gameOverScreen;

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
            GameIsPaused = true;
            highlightCircle.localScaleTransition_XY(new Vector2(2f, 2f), 2f, LeanEase.Decelerate)
                .JoinDelayTransition(2)
                .localScaleTransition_XY(Vector2.zero, .5f, LeanEase.Accelerate)
                .JoinDelayTransition(.5f);

            gameOverScreen.alphaTransition(1, 1)
                .interactableTransition(true, 1)
                .blocksRaycastsTransition(true, 1);
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
            if (GameIsPaused)
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
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        //necessary for to halt spawners, player movement, etc
        //not currenlty implemented
        public bool IsGamePaused()
        {
            return GameIsPaused;
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