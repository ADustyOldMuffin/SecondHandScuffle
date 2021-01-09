using Lean.Transition;
using UnityEngine;
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
            if (AudioManager.Instance is null || EventBus.Instance is null)
                return;
            
            EventBus.Instance.OnPlayerDeath += OnPlayerDeath;
            
            //can't get this to work. wip. learning input system. I did add P and space as pause options.
            //InputManager.Instance.InputMaster.UI.Pause.performed += OnPauseKey;
            enemySpawners = FindObjectsOfType<EnemySpawner>();

            musicVolumeSlider.value = AudioManager.GetMusicLinearVolume();
            sfxVolumeSlider.value = AudioManager.GetSfxLinearVolume();
        }

        private void OnDisable()
        {
            if (AudioManager.Instance is null || EventBus.Instance is null)
                return;
            
            EventBus.Instance.OnPlayerDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath()
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

        private void Start()
        {
            StartCoroutine(AudioManager.Instance?.PlayBattleTheme());
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