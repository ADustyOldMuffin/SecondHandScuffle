using System.Collections;
using Pathfinding;
using Entities;
using UnityEngine;
using Managers;

namespace States
{
    public class MeleeAttack : IState
    {
        private readonly Enemy _enemy;
        private readonly Animator _enemyAnimator;

        float attackCounter = 1;

        public MeleeAttack(Enemy enemy, Animator enemyAnimator)
        {
            _enemy = enemy;
            _enemyAnimator = enemyAnimator;
        }

        public void Tick()
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0f)
            {
                Debug.Log("launch melee attack");
                attackCounter = 1;
            }
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