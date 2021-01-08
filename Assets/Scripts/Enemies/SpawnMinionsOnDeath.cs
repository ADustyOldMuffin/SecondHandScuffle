using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinionsOnDeath : MonoBehaviour
{
    [SerializeField] GameObject[] minions;
    GameObject orphanParent;
    const string ORPHAN_PARENT_NAME = "Orphaned Minions";


    private void Start()
    {
        CreateOrphanMinionParent();
    }

    //for organisational purposes
    private void CreateOrphanMinionParent()
    {
        orphanParent = GameObject.Find(ORPHAN_PARENT_NAME);
        if (!orphanParent)
        {
            orphanParent = new GameObject(ORPHAN_PARENT_NAME);
        }
    }


    public void OnDeathSpawn()
    {
        //spawn first
        for(int i = 0; i < minions.Length; i++)
        {
            GameObject minion = Instantiate(minions[Random.Range(0, minions.Length)],
        transform.position,
        transform.rotation) as GameObject;

            minion.transform.parent = orphanParent.transform;
        }

        //now die
        Destroy(gameObject);
    }
}
