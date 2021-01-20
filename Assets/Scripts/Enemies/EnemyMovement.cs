using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Managers;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {

        [SerializeField] Rigidbody2D myRGB;
        [SerializeField] public Animator myAnimator;

        // Update is called once per frame
        void FixedUpdate()
        {
            FacethePlayer();
        }

        private void FacethePlayer()
        {
            if (LevelManager.Instance.Player != null) 
            { 
            //Debug.Log(Mathf.Sign(target.transform.position.x - transform.position.x).ToString());
            transform.localScale = new Vector2(Mathf.Sign(LevelManager.Instance.Player.transform.position.x - transform.position.x), 1);
            }

        }

        [SerializeField] public float thrust = .1f;
        [SerializeField] public float knockTime = .1f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PlayerProjectile"))
            {
                Vector2 difference = new Vector2(
                    (transform.position.x - LevelManager.Instance.Player.transform.position.x), 
                    (transform.position.y - LevelManager.Instance.Player.transform.position.y));
                difference = difference * thrust;
                //Debug.Log(difference.ToString());
                myRGB.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo());
            }
        }

        private IEnumerator KnockCo()
        {
            yield return new WaitForSeconds(knockTime);
            myRGB.velocity = Vector2.zero;
            myAnimator.SetBool("hasBeenHit", false);

        }

        /**
         * used in melee attack to stop jump back
         * *coroutines cannot be run from static non-monobehaviour classes
         * **/
        public void EndJumpBack(float jumpBackTime)
        {
            StartCoroutine(StopMovingBack(jumpBackTime));
        }

        private IEnumerator StopMovingBack(float jumpBackTime)
        {
            yield return new WaitForSeconds(jumpBackTime);
            myRGB.velocity = Vector2.zero;
        }



    }
}
