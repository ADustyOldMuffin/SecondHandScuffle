using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class SpawnMinions : MonoBehaviour
{

    [SerializeField] GameObject[] minions;

    [SerializeField] float spawnTimerMin = 2f;
    [SerializeField] float spawnTimerMax = 10f;
    [SerializeField] float range = 6f;
    private bool inRange = false;
    bool spawn = true;

    IEnumerator Start()
    {
        while (spawn)
        {

            yield return new WaitForSecondsRealtime(Random.Range(spawnTimerMin, spawnTimerMax));
            InRange();
            if (spawn && inRange) { SpawnEnemies(); }

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

    private void InRange()
    {
        float distance = Mathf.Abs(Vector3.Distance(LevelManager.Instance.Player.transform.position, transform.position));
        //Debug.Log(distance.ToString());
        //Debug.Log(distance.ToString());
        if (distance <= range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
}
