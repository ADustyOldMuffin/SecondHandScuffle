using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private static Dictionary<Level, string> Levels = new Dictionary<Level, string>()
        {
            {Level.Test, "TestScene"}
        };

        protected override void Awake()
        {
            base.Awake();

            if (levelToLoadOnStart == Level.None)
                return;

            SceneManager.LoadScene(Levels[levelToLoadOnStart]);
        }

        public void ChangeLevel(Level newLevel)
        {
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
    }
}