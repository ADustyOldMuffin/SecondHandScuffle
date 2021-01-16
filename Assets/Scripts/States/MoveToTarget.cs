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
                //Debug.Log("I am sitting on target"); 
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
            //_graphics.localScale = force.x >= 0.01f ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);
        }

        public void OnEnter()
        {
            //Debug.Log("I am moving now");
        }

        public void OnExit()
        {
            //Debug.Log("I am not moving anymore");
        }
    }
}