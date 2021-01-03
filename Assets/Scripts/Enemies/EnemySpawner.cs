using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    [SerializeField] float spawnTimerMin = 2f;
    [SerializeField] float spawnTimerMax = 10f;

    bool spawn = true;
    int upperRangeOfEnemyChoice = 1;

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
    public void UpdateSpawnRange(int mutation)
    {
        if(mutation == 0)
        {
            upperRangeOfEnemyChoice = 1;
            spawnTimerMin = 5f;
            spawnTimerMax = 16f;
        }
        else if (mutation == 1 || mutation == 2)
        {
            upperRangeOfEnemyChoice = 2;
            spawnTimerMin = 4f;
            spawnTimerMax = 14f;

        }
        else if (mutation == 3 || mutation == 4)
        {
            upperRangeOfEnemyChoice = 3;
            spawnTimerMin = 3f;
            spawnTimerMax = 12f;
        }
        else if (mutation == 5 || mutation == 6)
        {
            upperRangeOfEnemyChoice = 4;
            spawnTimerMin = 2f;
            spawnTimerMax = 8f;
        }
        else if (mutation >= 7)
        {
            upperRangeOfEnemyChoice = Enemies.Length;
            spawnTimerMin = 1f;
            spawnTimerMax = 4f;
        }
    }
}
