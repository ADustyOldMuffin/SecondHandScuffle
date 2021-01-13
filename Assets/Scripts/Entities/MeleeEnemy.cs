using System;
using Pathfinding;
using States;
using UnityEngine;

namespace Entities
{
    public class MeleeEnemy : Enemy
    {
        protected override void Awake()
        {
            base.Awake();
            
            var search = new SearchForTarget(this,"Player");
            var moveToTarget = new MoveToTarget(this, moveSpeed, nextWaypointDistance
                , myRigidbody, spriteContainer);
            var idle = new Idle();
            
            AddTransition(search, moveToTarget, TargetFound());
            AddTransition(search, idle, TargetMissing());
            //AddTransition(moveToTarget, search, IsStuck());
            AddTransition(moveToTarget, search, TargetMissing());
            AddTransition(idle, search, ShouldAttack());

            AddAnyTransition(idle, () => !shouldAttackPlayer);
            
            StateMachine.SetState(search);

            Func<bool> TargetMissing() => () => Target == null;
            Func<bool> IsStuck() => () => moveToTarget.TimeStuck > timeTillSearchWhenStuck;
            Func<bool> TargetFound() => () => Target != null;
            Func<bool> ShouldAttack() => () => shouldAttackPlayer;
            
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
                Debug.Log("Checking path!");
            }
                
        }
    }
}