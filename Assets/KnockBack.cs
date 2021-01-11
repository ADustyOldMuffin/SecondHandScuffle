using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] public float thrust = 1f;
    [SerializeField] public float knockTime = .1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            Debug.Log("first " + enemy.velocity.ToString());
            if (enemy != null)
            {
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                Debug.Log("second " + enemy.velocity.ToString());
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            Debug.Log("third " + enemy.velocity.ToString());
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            Debug.Log("fourth " + enemy.velocity.ToString());
        }

    }

}
