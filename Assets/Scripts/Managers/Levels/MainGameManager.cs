using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Managers.Levels
{
    public class MainGameManager : MonoBehaviour
    {
        private void Awake()
        {
            LevelManager.OnPlayerDeath += PlayerDied;
        }

        private void PlayerDied()
        {
            Debug.Log("About to die for real");
            LevelManager.Instance.LoadLevel((int)Level.GameOver);
        }

        private IEnumerator WaitAndGameOver()
        {
            yield return new WaitForSeconds(2);
            LevelManager.Instance.LoadLevel((int)Level.GameOver);
        }
    }
}