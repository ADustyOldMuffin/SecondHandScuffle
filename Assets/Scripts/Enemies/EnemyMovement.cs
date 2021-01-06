using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float stoppingDistance = 2f;
        Rigidbody2D myRGB;
        Vector2 aim;

        [Tooltip("if enemy is melee true, if not melee false")]
        [SerializeField] bool isMeleeEnemy = true;


        //cached reference to player
        PlayerHealth target;

        void Awake()
        {
            UpdateTargeting();
            myRGB = GetComponent<Rigidbody2D>();

        }

        // Start is called before the first frame update
        void Start()
        {
            MoveTowardsTarget();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            UpdateTargeting();
            MoveTowardsTarget();
            if (!isMeleeEnemy)
            {
                CheckToStopAndAttack();
            }
        }

        void UpdateTargeting()
        {
            if (GameObject.FindObjectOfType<PlayerHealth>() != null)
            {
                //find player in scene by player health script
                target = FindObjectOfType<PlayerHealth>();
                aim = (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
            }
            else //if no enemy stop
            {
                moveSpeed = 0;
                aim = new Vector2(0, 0);
            }
        }

        void MoveTowardsTarget()
        {
            var newPosition = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.fixedDeltaTime);
            newPosition = PixelMovementUtility.PixelPerfectClamp(newPosition, 16);
            myRGB.MovePosition(newPosition);
        }

        void CheckToStopAndAttack()
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= stoppingDistance)
            {
                moveSpeed = 0;
                //Attack
            }
        }

        public void PushBackAfterMeleeAttack()
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -1 * moveSpeed * Time.deltaTime);

        }

    }
}
