using System;
using Pathfinding;
using States;
using UnityEngine;

namespace Entities
{
    public class SpawnerEnemy : Enemy
    {
        [SerializeField] private float maxDistance = 6f;

        [SerializeField] GameObject[] minions;

        [SerializeField] float spawnTimerMin = 2f;
        [SerializeField] float spawnTimerMax = 10f;

        [SerializeField] private Animator enemyAnimator;

        protected override void Awake()
        {
            base.Awake();

            var search = new SearchForTarget(this, "Player");
            var MoveToTarget = new MoveToTarget(this, moveSpeed, nextWaypointDistance
                , myRigidbody, spriteContainer);
            var idle = new Idle();
            var SpawnEnemy = new SpawnMinionState(this, spawnTimerMin, spawnTimerMax, enemyAnimator);

            AddTransition(search, MoveToTarget, TargetFound());
            AddTransition(search, idle, TargetMissing());
            //AddTransition(moveToTarget, search, IsStuck());
            AddTransition(MoveToTarget, search, TargetMissing());
            AddTransition(idle, search, ShouldAttack());
            AddTransition(MoveToTarget, SpawnEnemy, InRangeToAttack());
            AddTransition(SpawnEnemy, search, TargetOutofRange());

            AddAnyTransition(idle, () => !shouldAttackPlayer);

            StateMachine.SetState(search);

            Func<bool> TargetMissing() => () => Target == null;
            Func<bool> IsStuck() => () => MoveToTarget.TimeStuck > timeTillSearchWhenStuck;
            Func<bool> TargetFound() => () => Target != null;
            Func<bool> ShouldAttack() => () => shouldAttackPlayer;
            Func<bool> InRangeToAttack() => () => Target != null
            && Mathf.Abs(Vector3.Distance(Target.transform.position, transform.position)) <= maxDistance;
            Func<bool> TargetOutofRange() => () => Target != null && Mathf.Abs(Vector3.Distance(Target.transform.position, transform.position)) > maxDistance;

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
