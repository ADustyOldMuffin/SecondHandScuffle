using System.Linq;
using Entities;
using UnityEngine;

namespace States
{
    public class SearchForTarget : IState
    {
        private readonly Enemy _enemy;
        private readonly string _targetTag;

        public SearchForTarget(Enemy enemy, string targetTag)
        {
            _enemy = enemy;
            _targetTag = targetTag;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            _enemy.Target = GameObject.FindGameObjectsWithTag(_targetTag).FirstOrDefault();
        }
    }
}