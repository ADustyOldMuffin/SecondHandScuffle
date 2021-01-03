using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons;

namespace Managers
{
    public class LevelManager : SingletonBehavior<LevelManager>
    {
        /// <summary>
        /// The level that will load on the start of this object.
        /// </summary>
        [SerializeField] private Level levelToLoadOnStart = Level.None;

        /// <summary>
        /// All the levels in the project in the format of Key - Level Enum, Value - Scene name
        /// </summary>
        private static readonly Dictionary<Level, string> Levels = new Dictionary<Level, string>()
        {
            {Level.Test, "TestScene"},
            {Level.OpeningAnimation, "OpeningAnimation"},
            {Level.MainMenu, "MainMenu"},
            {Level.Options, "Options"},
            {Level.MainGame, "MainGame"},
            {Level.GameOver, "GameOver"}
        };

        public delegate void PlayerDeathAction();
        public static event PlayerDeathAction OnPlayerDeath;

        public delegate void PlayerHurtAction();
        public static event PlayerHurtAction OnPlayerHurt;
        
        public delegate void PlayerWeaponChange(BaseWeapon weapon);
        public static event PlayerWeaponChange OnPlayerWeaponChange;

        public delegate void PlayerScoreChange();
        public static event PlayerScoreChange OnPlayerScoreChange;

        public delegate void PlayerWeaponReturn();
        public static event PlayerWeaponReturn OnPlayerWeaponReturn;

        public int PlayerScore { get; private set; }

        public GameObject Player { get; private set; }




        protected override void Awake()
        {
            base.Awake();

            if (levelToLoadOnStart == Level.None)
                return;

            SceneManager.LoadScene(Levels[levelToLoadOnStart]);
        }

        public void ChangeLevel(Level newLevel)
        {
            // Make sure we have the level.
            if (!Levels.TryGetValue(newLevel, out var sceneName))
                return;

            StartCoroutine(LoadScene(sceneName));
        }

        //cannot reference Level from inspector. put this in place so that buttons were usable. on click needs a public method. change level was not avaliable
        public void LoadLevel(int newLevel)
        {
            // Make sure we have the level.
            if (!Levels.TryGetValue((Level) newLevel, out var levelName))
            {
                throw new ArgumentException($"Level does not exist with the value of {newLevel}", nameof(newLevel));
            }

            StartCoroutine(LoadScene(levelName));
        }

        private static IEnumerator LoadScene(string sceneName)
        {
            var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!asyncLoadLevel.isDone)
            {
                // TODO Show loading screen?
                yield return null;
            }

            // Wait for all objects to be instantiated
            yield return new WaitForEndOfFrame();
            
            // Do anything else that needs to be done after loading a level
        }

        public static void PlayerDied()
        {
            InputManager.Instance.InputMaster.Player.Disable();
            if (OnPlayerDeath != null)
                OnPlayerDeath();
        }

        public static void HurtPlayer(int amount)
        {
            if (OnPlayerHurt != null)
                OnPlayerHurt();
        }

        public static void PlayerScoreChanged()
        {
            if (OnPlayerScoreChange != null)
                OnPlayerScoreChange();
        }

        public static void PlayerWeaponChanged(BaseWeapon weapon)
        {
            if (OnPlayerWeaponChange != null)
                OnPlayerWeaponChange(weapon);
        }

        public static void PlayerWeaponReturned()
        {
            if (OnPlayerWeaponReturn != null)
                OnPlayerWeaponReturn();
        }

        public void IncreaseScore(int amount)
        {
            PlayerScore += amount;
            PlayerScoreChanged();

        }

        public void SetPlayerScore(int amount)
        {
            PlayerScore = amount;
            PlayerScoreChanged();
        }

        public void SetPlayer(GameObject player)
        {
            Player = player;
        }


    }
}