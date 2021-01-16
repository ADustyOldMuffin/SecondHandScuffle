using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Managers;

public class DynamicSorting : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    void Awake()
    {
        //add all objects in obstacles layer into the array
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        GameObject closestObstacle = SortDynamically();
        Debug.Log(closestObstacle.name);
        SortRelativeToPlayer(closestObstacle);
    }

    GameObject SortDynamically()
    {
        GameObject closestObstacle = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (GameObject o in obstacles)
        {
            Vector3 directionToTarget = o.transform.position - LevelManager.Instance.Player.transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestObstacle = o;
            }
        }
        return closestObstacle;

    }

    void SortRelativeToPlayer(GameObject gameObject)
    {
        if (LevelManager.Instance is null)
            return;

        var playerY = LevelManager.Instance.Player.transform.position.y;

        if (playerY - transform.position.y > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }
}

