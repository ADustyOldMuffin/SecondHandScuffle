using Lean.Transition;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Weapons;

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

        [SerializeField] private Transform weaponStatusContainer;

        [Header("Lean Animations")]
        [SerializeField] private Transform highlightCircle;
        [SerializeField] private CanvasGroup gameOverScreen;

        public SceneTransitionLoader sceneTransitionLoader;

        private GameObject _currentStatusObject;

        private void Awake()
        {
            if (InputManager.Instance is null)
                return;

            EventBus.Instance.OnPlayerDeath += OnPlayerDeath;
            EventBus.Instance.OnWeaponChange += OnWeaponMutation;

            InputManager.Instance.InputMaster.UI.TogglePause.performed += OnPauseKey;

            enemySpawners = FindObjectsOfType<EnemySpawner>();

            musicVolumeSlider.value = AudioManager.GetMusicLinearVolume();
            sfxVolumeSlider.value = AudioManager.GetSfxLinearVolume();
        }

        private void OnEnable()
        {
            if (InputManager.Instance is null)
                return;

            InputManager.Instance.InputMaster.UI.TogglePause.Enable();
        }

        private void OnDisable()
        {
            if (AudioManager.Instance is null || EventBus.Instance is null)
                return;
            
            EventBus.Instance.OnPlayerDeath -= OnPlayerDeath;

            if (InputManager.Instance is null)
                return;
            InputManager.Instance.InputMaster.UI.TogglePause.Disable();
            InputManager.Instance.InputMaster.UI.TogglePause.performed -= OnPauseKey;
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

        private void OnWeaponMutation(BaseWeapon originalWeapon, BaseWeapon newWeapon)
        {
            if(_currentStatusObject != null)
                Destroy(_currentStatusObject);
            
            Debug.Log("New weapon status!");

            var newStatusObject = Instantiate(newWeapon.weaponStatusObject);
            var rect = newStatusObject.GetComponent<RectTransform>();
            rect.SetParent(weaponStatusContainer);
            rect.anchoredPosition = Vector3.zero;
            rect.localScale = Vector3.one;
            newStatusObject.GetComponent<IWeaponStatusUI>().SetInitialStatus();
            _currentStatusObject = newStatusObject;
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
        private void OnPauseKey(InputAction.CallbackContext context)
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

        public void LoadLevelFromPauseMenu(int level)
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
            pauseMenuUI.SetActive(false);
            sceneTransitionLoader.TransitionToNewScene(level);
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