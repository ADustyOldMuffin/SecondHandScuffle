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

        public static void ChangeLevel(Level newLevel)
        {
            SceneManager.LoadScene(Levels[newLevel]);
        }
    }
}