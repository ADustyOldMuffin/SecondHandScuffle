using System.Collections;
using Pathfinding;
using Entities;
using UnityEngine;
using Managers;

namespace States
{
    public class SpawnMinionState : IState
    {
        private readonly Enemy _enemy;
        private readonly Animator _enemyAnimator;
        private readonly float _spawnTimerMin;
        private readonly float _spawnTimerMax;

        float spawnCounter;


        public SpawnMinionState(Enemy enemy, float spawnTimerMin, float spawnTimerMax, Animator enemyAnimator)
        {
            _enemy = enemy;
            _spawnTimerMin = spawnTimerMin;
            _spawnTimerMax = spawnTimerMax;
            spawnCounter = Random.Range(_spawnTimerMin, _spawnTimerMax);
            _enemyAnimator = enemyAnimator;
        }

        public void Tick()
        {
            CountDownAndShoot();
        }

        private void CountDownAndShoot()
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0f)
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            _enemyAnimator.SetTrigger("isSpawning");
            spawnCounter = Random.Range(_spawnTimerMin, _spawnTimerMax);
        }

        public void OnEnter()
        {
            //set animator to attack
            //Debug.Log("I am attacking now");
           // _enemyAnimator.SetBool("isAttacking", true);
        }

        public void OnExit()
        {
            //Debug.Log("I am no longer attacking");
            //set animator to not attack
            //_enemyAnimator.SetBool("isAttacking", false);
        }
    }
}
