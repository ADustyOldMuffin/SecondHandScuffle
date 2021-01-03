using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Managers.Levels
{
    public class MainGameManager : MonoBehaviour
    {
        EnemySpawner[] enemySpawners;
        
        private void Awake()
        {
            if (LevelManager.Instance == null)
                return;
            
            LevelManager.Instance.SetPlayerScore(0);
            LevelManager.OnPlayerDeath += PlayerDied;
            enemySpawners = FindObjectsOfType<EnemySpawner>();
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
    }
}