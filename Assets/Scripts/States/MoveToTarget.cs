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
        
        //private Path _path;
        private int _currentWaypoint;
        private Coroutine _findPathRoutine;
        private Vector2 _lastPosition;
        private Vector2 _targetLastPosition;

        public float TimeStuck;
        public bool ReachedEndOfPath;

        public MoveToTarget(Enemy enemy, float speed, float nextWaypointDistance, ref int currentWaypoint, Rigidbody2D rigidbody, Transform graphics)
        {
            _enemy = enemy;
            _speed = speed;
            _nextWaypointDistance = nextWaypointDistance;
            _rigidbody = rigidbody;
            _graphics = graphics;
            _currentWaypoint = currentWaypoint;
        }

        public void Tick()
        {
            if (_enemy.CurrentPath is null)
                return;

            if (_currentWaypoint >= _enemy.CurrentPath.vectorPath.Count)
            {
                Debug.Log("End of path reached.");
                ReachedEndOfPath = true;
                return;
            }

            ReachedEndOfPath = false;
            
            var position = _rigidbody.position;
            var direction = ((Vector2)_enemy.CurrentPath.vectorPath[_currentWaypoint] - position).normalized;
            var force = direction * (_speed * Time.fixedDeltaTime);
            
            _rigidbody.AddForce(force);

            var distance = Vector2.Distance(position, _enemy.CurrentPath.vectorPath[_currentWaypoint]);

            if (distance < _nextWaypointDistance)
            {
                Debug.Log("Increasing waypoint");
                _currentWaypoint++;
            }

            if (Vector2.Distance(_enemy.transform.position, _lastPosition) <= .05f)
                TimeStuck += Time.fixedDeltaTime;

            _lastPosition = _enemy.transform.position;
            _graphics.localScale = force.x >= 0.01f ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }
    }
}