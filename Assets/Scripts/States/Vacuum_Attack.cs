using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Vacuum_Attack : MonoBehaviour
{
    [SerializeField] public float pull = 1f;
    [SerializeField] public float knockTime = .1f;
    public void SuckUpPlayer()
    {
        if(LevelManager.Instance.Player != null)
        {
            Debug.Log("ready to suck");
            Rigidbody2D playerBody = LevelManager.Instance.Player.GetComponent<Rigidbody2D>();
            Vector2 direction = transform.position - LevelManager.Instance.Player.transform.position;
            direction = direction.normalized;
            playerBody.AddForce(direction * pull);
            //StartCoroutine(StopCo(playerBody));
        }
    }

    private IEnumerator StopCo(Rigidbody2D playerBody)
    {
        yield return new WaitForSeconds(knockTime);
        playerBody.velocity = Vector2.zero;

    }
}
