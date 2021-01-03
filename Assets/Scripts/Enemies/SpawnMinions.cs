using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinions : MonoBehaviour
{

    [SerializeField] GameObject[] minions;

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
        GameObject minion = Instantiate(minions[Random.Range(0, minions.Length)],
                transform.position,
                transform.rotation) as GameObject;

        minion.transform.parent = transform;

    }
}
