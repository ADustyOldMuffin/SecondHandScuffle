using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    [SerializeField] float spawnTimerMin = 5f;
    [SerializeField] float spawnTimerMax = 16f;

    bool spawn = true;
    int upperRangeOfEnemyChoice = 1;

    private void Awake()
    {
        LevelManager.OnPlayerScoreChange += UpdateSpawnRange;
    }

    IEnumerator Start()
    {
        while (spawn)
        {

            yield return new WaitForSecondsRealtime(Random.Range(spawnTimerMin, spawnTimerMax));
            if (spawn) { SpawnEnemies(); }

        }
    }


    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnEnemies()
    {
        GameObject enemy = Instantiate(Enemies[Random.Range(0, upperRangeOfEnemyChoice)],
                transform.position,
                transform.rotation) as GameObject;

        enemy.transform.parent = transform;

    }

    //as more mutations survived increase range of possible enemies and change spawning range
    public void UpdateSpawnRange()
    {
        int mutation = LevelManager.Instance.PlayerScore;
        //Debug.Log("Updating spawner");
        if(mutation == 0)
        {
            Debug.Log("Updating spawner 0");
            upperRangeOfEnemyChoice = 1;
            spawnTimerMin = 5f;
            spawnTimerMax = 16f;
        }
        else if (mutation == 1 || mutation == 2)
        {
            //Debug.Log("Updating spawner 1");
            upperRangeOfEnemyChoice = 2;
            spawnTimerMin = 4f;
            spawnTimerMax = 14f;

        }
        else if (mutation == 3 || mutation == 4)
        {
            //Debug.Log("Updating spawner 3");
            upperRangeOfEnemyChoice = 3;
            spawnTimerMin = 3f;
            spawnTimerMax = 12f;
        }
        else if (mutation == 5 || mutation == 6)
        {
            //Debug.Log("Updating spawner 5");
            upperRangeOfEnemyChoice = 4;
            spawnTimerMin = 2f;
            spawnTimerMax = 8f;
        }
        else if (mutation >= 7)
        {
            //Debug.Log("Updating spawner 7");
            upperRangeOfEnemyChoice = Enemies.Length;
            spawnTimerMin = 1f;
            spawnTimerMax = 4f;
        }
    }
}
