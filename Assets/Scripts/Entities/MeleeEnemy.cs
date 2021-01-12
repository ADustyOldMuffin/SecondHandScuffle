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
            var moveToTarget = new MoveToTarget(this, moveSpeed, nextWaypointDistance, ref CurrentIndex, myRigidbody, spriteContainer);
            
            AddTransition(search, moveToTarget, () => Target != null);
            AddTransition(moveToTarget, search, () => moveToTarget.TimeStuck > timeTillSearchWhenStuck);
            
            StateMachine.SetState(search);
            
            InvokeRepeating(nameof(UpdatePath), 0f, .5f);
        }

        private void OnPathComplete(Path p)
        {
            if (p.error)
                return;

            CurrentPath = p;
            CurrentIndex = 0;
        }

        public void UpdatePath()
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(myRigidbody.position, Target.transform.position, OnPathComplete);
                Debug.Log("Checking path!");
            }
                
        }
    }
}