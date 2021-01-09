using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using Player;
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
        
        public GameObject Player { get; private set; }

        private void Start()
        {
            if (levelToLoadOnStart == Level.None || EventBus.Instance is null)
                return;

            EventBus.Instance.OnLevelChangeRequest += ChangeLevel;
            
            EventBus.Instance?.ChangeLevel(levelToLoadOnStart);
        }

        public void ChangeLevel(Level newLevel)
        {
            // Make sure we have the level.
            if (!Levels.TryGetValue(newLevel, out var sceneName))
                return;

            StartCoroutine(LoadScene(sceneName));
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

        public void SetPlayer(GameObject player)
        {
            Player = player;
        }

        public int GetPlayerCurrentHealth()
        {
            if (Player is null || !Player.TryGetComponent(out PlayerHealth health))
                return 0;

            return health.GetPlayerHealth();
        }
    }
}