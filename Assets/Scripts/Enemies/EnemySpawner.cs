using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    [SerializeField] float spawnTimerMin = 2f;
    [SerializeField] float spawnTimerMax = 10f;

    bool spawn = true;

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
        GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)],
                transform.position,
                transform.rotation) as GameObject;

        enemy.transform.parent = transform;

    }
}
