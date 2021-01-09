using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Managers.Levels;

public class SpawnMinions : MonoBehaviour
{

    [SerializeField] GameObject[] minions;
    public Animator myAnimator;
    [SerializeField] float spawnTimerMin = 2f;
    [SerializeField] float spawnTimerMax = 10f;
    [SerializeField] float range = 6f;
    private bool inRange = false;
    bool spawn = true;
    MainGameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<MainGameManager>();
    }

    IEnumerator Start()
    {
        while (spawn)
        {

            yield return new WaitForSecondsRealtime(Random.Range(spawnTimerMin, spawnTimerMax));
            InRange();
            if (spawn && inRange && !gameManager.IsGamePaused()) { myAnimator.SetTrigger("isSpawning"); }

        }
    }


    public void StopSpawning()
    {
        spawn = false;
    }

    public void Attack()
    {
        SpawnMinion();
    }

    private void SpawnMinion()
    {
        GameObject minion = Instantiate(minions[Random.Range(0, minions.Length)],
                transform.position,
                transform.rotation) as GameObject;

        //minion's parent is the minion spawner. if the spawner dies, so do it's children
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
