using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.Projectiles;

public class EnemyFireProjectile : MonoBehaviour
{
    [Header("Enemy Projectile")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] EnemyProjectile projectile;

    


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                Fire();
                shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
    }

    private void Fire()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
