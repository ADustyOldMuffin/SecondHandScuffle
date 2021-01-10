using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Managers;

namespace Enemies
{
    public class EnemyFireProjectile : MonoBehaviour
    {
        [Header("Enemy Projectile")]
        float shotCounter;
        [SerializeField] float minTimeBetweenShots = 1f;
        [SerializeField] float maxTimeBetweenShots = 5f;
        [SerializeField] EnemyProjectile projectile;
        [SerializeField] float range = 6f;
        private bool inRange = false;
        public Animator myAnimator;
        public EnemyMovement movement;


    // Start is called before the first frame update
    void Start()
        {
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            InRange();
            CountDownAndShoot();
        }

        private void CountDownAndShoot()
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f && inRange)
            {
                Attack();
            }
        }

        private void Attack()
        {
            Debug.Log("Boombox is attacking");
            myAnimator.SetTrigger("isAttacking");
        }

        public void Fire()
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            Debug.Log("reset");
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
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
}
