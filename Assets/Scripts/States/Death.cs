using Entities;
using UnityEngine;

namespace States
{
    public class Death : IState
    {
        private readonly Enemy _enemy;
        private readonly Animator _enemyAnimator;

        public Death(Enemy enemy, Animator enemyAnimator)
        {
            _enemy = enemy;
            _enemyAnimator = enemyAnimator;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            //Death Animation
            Debug.Log("I am about to die");
            Object.Destroy(_enemy.gameObject, 2f);
        }

        public void OnExit()
        {
            //Debug.Log("I am not moving anymore");
        }
    }
}
