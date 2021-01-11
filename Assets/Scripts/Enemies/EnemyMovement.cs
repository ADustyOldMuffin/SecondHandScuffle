using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Managers;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] float baseMoveSpeed = 5f;
        private float currenMoveSpeed;
        [SerializeField] float stoppingDistance = 2f;
        Rigidbody2D myRGB;
        Vector2 aim;

        //[Tooltip("if enemy is melee true, if not melee false")]
        //[SerializeField] bool isMeleeEnemy = true;

        void Awake()
        {
            UpdateTargeting();
            myRGB = GetComponent<Rigidbody2D>();
            currenMoveSpeed = baseMoveSpeed;
        }

        // Start is called before the first frame update
        void Start()
        {
            FacePlayer();
            MoveTowardsTarget();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            /**
            if (!isMeleeEnemy)
            {
                CheckToStopAndAttack();
            }
            **/
                FacePlayer();
                UpdateTargeting();
                MoveTowardsTarget();
        }

        void UpdateTargeting()
        {
            if (FindObjectOfType<PlayerHealth>() != null)
            {
                //find player in scene by player health script
                //target = FindObjectOfType<PlayerHealth>();
                aim = (LevelManager.Instance.Player.transform.position - transform.position).normalized * currenMoveSpeed * Time.deltaTime;
            }
            else //if no enemy stop
            {
                currenMoveSpeed = 0;
                aim = new Vector2(0, 0);
            }
        }

        void MoveTowardsTarget()
        {
            var newPosition = Vector2.MoveTowards(transform.position, LevelManager.Instance.Player.transform.position, currenMoveSpeed * Time.fixedDeltaTime);
            newPosition = PixelMovementUtility.PixelPerfectClamp(newPosition, 16);
            myRGB.MovePosition(newPosition);
        }

        /**
        void CheckToStopAndAttack()
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= stoppingDistance)
            {
                Debug.Log("should stop");
                currenMoveSpeed = 0f;
                //Attack
            }
            else
            {
                currenMoveSpeed = baseMoveSpeed;
            }
        }**/

        public void PushBackAfterMeleeAttack()
        {
            transform.position = Vector2.MoveTowards(transform.position, LevelManager.Instance.Player.transform.position, -1 * currenMoveSpeed * Time.deltaTime);

        }

        public void ResetMoveSpeed()
        {
            currenMoveSpeed = baseMoveSpeed;
        }
        public void ZeroMoveSpeed()
        {
            currenMoveSpeed = 0;
        }
        /**
        public void SetCurrentMoveSpeed(float newSpeed)
        {
            currenMoveSpeed = newSpeed;
        }
        **/

        private void FacePlayer()
        {

        }
    }
}
