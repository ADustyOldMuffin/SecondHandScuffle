using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Managers;

public class DynamicSorting : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private SpriteRenderer enemyRenderer;
    void Awake()
    {
        //add all objects in obstacles layer into the array
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        GameObject closestObstacle = SortDynamically();
        //Debug.Log(closestObstacle.name);
        SortRelativeToPlayer(closestObstacle);
    }

    GameObject SortDynamically()
    {
        GameObject closestObstacle = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (GameObject o in obstacles)
        {
            Vector3 directionToTarget = o.transform.position - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestObstacle = o;
            }
        }
        return closestObstacle;

    }

    void SortRelativeToPlayer(GameObject obstacle)
    {
        var obstacleY = obstacle.transform.position.y;

        if (obstacleY - transform.position.y > 0)
        {
            enemyRenderer.sortingOrder = obstacle.GetComponent<SpriteRenderer>().sortingOrder + 3;
            //Debug.Log("sort higher " + transform.position.y.ToString() + " " + enemyRenderer.sortingOrder.ToString() + " " + obstacle.name + " " + obstacle.transform.position.y.ToString() + " "  + obstacle.GetComponent<SpriteRenderer>().sortingOrder.ToString());
        }
        else
        {
            enemyRenderer.sortingOrder = obstacle.GetComponent<SpriteRenderer>().sortingOrder - 3;
            //Debug.Log("sort lower " + transform.position.y.ToString() + " " + enemyRenderer.sortingOrder.ToString() + " " + obstacle.name + " " + obstacle.transform.position.y.ToString() + " " + obstacle.GetComponent<SpriteRenderer>().sortingOrder.ToString());
        }
    }
}

