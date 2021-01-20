using System.Collections;
using Pathfinding;
using Entities;
using UnityEngine;
using Managers;
using Enemies;

namespace States
{
    public class MeleeAttack : IState
    {
        private readonly Enemy _enemy;
        private readonly Animator _enemyAnimator;
        private readonly Rigidbody2D _enemyRigidbody;
        private readonly float _enemyAttackCounterMax;
        private float _enemyAttackCounter;
        private readonly int _enemyAttack;
        private readonly float _enemyJumpBack;
        private readonly float _enemyJumpBackTime;

        public MeleeAttack(Enemy enemy, Animator enemyAnimator, Rigidbody2D enemyRGB, float attackCounter, int attackPower, float jumpBack, float jumpBackTime)
        {
            _enemy = enemy;
            _enemyAnimator = enemyAnimator;
            _enemyRigidbody = enemyRGB;
            _enemyAttackCounter = attackCounter;
            _enemyAttackCounterMax = attackCounter;
            _enemyAttack = attackPower;
            _enemyJumpBack = jumpBack;
            _enemyJumpBackTime = jumpBackTime;

        }

        public void Tick()
        {
            Debug.Log("in attack mode");
            _enemyAttackCounter -= Time.deltaTime;
            Debug.Log(_enemyAttackCounter.ToString());
            if (_enemyAttackCounter <= 0f)
            {
                Debug.Log("launch melee attack");
                EventBus.Instance.ChangePlayerHealthRequest(-_enemyAttack);
                _enemyAttackCounter = _enemyAttackCounterMax;
                JumpBack();
            }
        }

        private void JumpBack()
        {
            Vector2 difference = new Vector2(
            (_enemy.transform.position.x - LevelManager.Instance.Player.transform.position.x),
            (_enemy.transform.position.y - LevelManager.Instance.Player.transform.position.y));
            difference = difference * _enemyJumpBack;
            //Debug.Log(difference.ToString());
            _enemyRigidbody.AddForce(difference, ForceMode2D.Impulse);
            _enemy.GetComponent<EnemyMovement>().EndJumpBack(_enemyJumpBackTime);
        }

        public void OnEnter()
        {
            //set animator to attack
            //Debug.Log("I am attacking now");
            //currently no attack animations
            //_enemyAnimator.SetBool("isAttacking", true);
        }

        public void OnExit()
        {
            //Debug.Log("I am no longer attacking");
            //set animator to not attack
            //currently no attack animations
            //_enemyAnimator.SetBool("isAttacking", false);
        }
    }
}
