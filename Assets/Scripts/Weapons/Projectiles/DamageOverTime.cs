using System;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.Projectiles
{
    public class DamageOverTime : MonoBehaviour
    {
        public int DamagePerTick { get; set; } = 1;
        public float TickTime { get; set; } = 1f;
        public int Ticks { get; set; } = 3;

        private int _ticksPassed = 0;
        private float _currentTime = 0.0f;
        private EnemyHealth _enemy;

        private void Awake()
        {
            // 
            if(!TryGetComponent<EnemyHealth>(out _enemy))
                Destroy(this);
        }

        private void FixedUpdate()
        {
            _currentTime += Time.fixedDeltaTime;

            if (_currentTime >= TickTime)
            {
                _currentTime = 0.0f;
                _ticksPassed += 1;
                _enemy.Damage(DamagePerTick, 0.5f, new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)));
            }
            
            if(_ticksPassed >= Ticks)
                Destroy(this);
        }

        public void ResetTicks()
        {
            _ticksPassed = 0;
        }
    }
}