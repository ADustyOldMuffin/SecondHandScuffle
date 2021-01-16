using System;
using Pathfinding;
using States;
using UnityEngine;
using Enemies;

namespace Entities
{
    public class SpawnerEnemy : Enemy
    {
        [SerializeField] private float maxDistance = 6f;

        [SerializeField] GameObject[] minions;

        [SerializeField] float spawnTimerMin = 2f;
        [SerializeField] float spawnTimerMax = 10f;

        [SerializeField] private Animator enemyAnimator;
        [SerializeField] private EnemyHealth enemyHealth;

        protected override void Awake()
        {
            base.Awake();

            var search = new SearchForTarget(this, "Player");
            var moveToTarget = new MoveToTarget(this, moveSpeed, nextWaypointDistance
                , myRigidbody, spriteContainer);
            var idle = new Idle();
            var spawnEnemy = new SpawnMinionState(this, spawnTimerMin, spawnTimerMax, enemyAnimator);
            var death = new Death(this, enemyAnimator);

            AddTransition(search, moveToTarget, TargetFound());
            AddTransition(search, idle, TargetMissing());
            //AddTransition(moveToTarget, search, IsStuck());
            AddTransition(moveToTarget, search, TargetMissing());
            AddTransition(idle, search, ShouldAttack());
            AddTransition(moveToTarget, spawnEnemy, InRangeToAttack());
            AddTransition(spawnEnemy, search, TargetOutofRange());

            AddAnyTransition(idle, () => !shouldAttackPlayer);
            AddAnyTransition(death, ReadyToDie());

            StateMachine.SetState(search);

            Func<bool> TargetMissing() => () => Target == null;
            Func<bool> IsStuck() => () => moveToTarget.TimeStuck > timeTillSearchWhenStuck;
            Func<bool> TargetFound() => () => Target != null;
            Func<bool> ShouldAttack() => () => shouldAttackPlayer;
            Func<bool> InRangeToAttack() => () => Target != null
            && Mathf.Abs(Vector3.Distance(Target.transform.position, transform.position)) <= maxDistance;
            Func<bool> TargetOutofRange() => () => Target != null && Mathf.Abs(Vector3.Distance(Target.transform.position, transform.position)) > maxDistance;
            Func<bool> ReadyToDie() => () => enemyHealth.GetCurrentHealth() <= 0;

            InvokeRepeating(nameof(UpdatePath), 0f, .5f);
        }

        private void OnPathComplete(Path p)
        {
            if (p.error)
                return;

            CurrentPath = p;
            currentIndex = 0;
        }

        public void UpdatePath()
        {
            if (seeker.IsDone() && shouldAttackPlayer && StateMachine.CurrentState.GetType() == typeof(MoveToTarget))
            {
                seeker.StartPath(myRigidbody.position, Target.transform.position, OnPathComplete);
            }

        }

        public void SpawnMinion()
        {
            GameObject minion = Instantiate(minions[UnityEngine.Random.Range(0, minions.Length)],
                    transform.position,
                    transform.rotation) as GameObject;

            //minion's parent is the minion spawner. if the spawner dies, so do it's children
            minion.transform.parent = transform;

        }
    }
}
