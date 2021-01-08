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
}
