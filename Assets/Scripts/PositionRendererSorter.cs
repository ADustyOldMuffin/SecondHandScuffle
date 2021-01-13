using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField] private int sortingOrderBase = 5000;
    private Renderer myRenderer;
    [SerializeField] private int offset = 0;
    [SerializeField] private bool runOnlyOnce = false;

    private float timer;
    private float timerMax = .1f;
   

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    //update about trasform has moved
    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = timerMax;

            myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
            if(runOnlyOnce)
            {
                Destroy(this);
            }
        }

    }
}
