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
            LevelManager.OnPlayerDeath += PlayerDied;
            enemySpawners = FindObjectsOfType<EnemySpawner>();
        }

        private void PlayerDied()
        {
            //Debug.Log("About to die for real");
            StopSpawners();
            //StartCoroutine(WaitAndGameOver());
            LevelManager.Instance.LoadLevel((int)Level.GameOver);
        }

        private IEnumerator WaitAndGameOver()
        {
            yield return new WaitForSeconds(2);
            LevelManager.Instance.LoadLevel((int)Level.GameOver);
        }

        private void StopSpawners()
        {
            for(int i = 0; i < enemySpawners.Length; i++)
            {
                enemySpawners[i].StopSpawning();
            }
        }
    }
}