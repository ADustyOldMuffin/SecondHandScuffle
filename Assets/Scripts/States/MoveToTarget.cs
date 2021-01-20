using System.Collections;
using Pathfinding;
using Entities;
using UnityEngine;

namespace States 
{
    public class MoveToTarget : IState
    {
        private readonly Enemy _enemy;
        private readonly Transform _graphics;
        private readonly float _speed, _nextWaypointDistance;
        private readonly Seeker _seeker;
        private readonly Rigidbody2D _rigidbody;
        
        private int _currentWaypoint;
        private Coroutine _findPathRoutine;
        private Vector2 _lastPosition;
        private Vector2 _targetLastPosition;

        public float TimeStuck;
        public bool ReachedEndOfPath;

        public MoveToTarget(Enemy enemy, float speed, float nextWaypointDistance, Rigidbody2D rigidbody, Transform graphics)
        {
            _enemy = enemy;
            _speed = speed;
            _nextWaypointDistance = nextWaypointDistance;
            _rigidbody = rigidbody;
            _graphics = graphics;
        }

        public void Tick()
        {
            if (_enemy.CurrentPath is null)
                return;

            if (_enemy.currentIndex >= _enemy.CurrentPath.vectorPath.Count)
            {
                ReachedEndOfPath = true;
                return;
            }

            ReachedEndOfPath = false;



            var position = _rigidbody.position;
            var newPos = Vector2.MoveTowards(position, (Vector2) _enemy.CurrentPath.vectorPath[_enemy.currentIndex],
                _speed * Time.fixedDeltaTime);

            _rigidbody.MovePosition(newPos);

            var distance = Vector2.Distance(position, _enemy.CurrentPath.vectorPath[_enemy.currentIndex]);

            if (distance < _nextWaypointDistance)
            {
                _enemy.currentIndex++;
            }

            if (Vector2.Distance(_enemy.transform.position, _lastPosition) <= .05f)
                TimeStuck += Time.fixedDeltaTime;

            _lastPosition = _enemy.transform.position;
        }

        public void OnEnter()
        {
            // Nothing to do
        }

        public void OnExit()
        {
            // Nothing to do
        }
    }
}