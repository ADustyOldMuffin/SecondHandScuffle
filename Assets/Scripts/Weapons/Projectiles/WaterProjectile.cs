using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class WaterProjectile : BaseProjectile
    {
        protected override int DamageAmount { get; } = 1;

        [SerializeField] private Component dotComponent;
        [SerializeField] private int dotTicks = 3;
        [SerializeField] private int tickDamage = 2;
        [SerializeField] private float tickTime = 1f;
        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private AnimationClip fireClip;

        private float _timer = 0.0f;

        private void Awake()
        {
            _timer = fireClip.length;
            animancer.Play(fireClip);
        }

        private void FixedUpdate()
        {
            _timer -= Time.fixedDeltaTime;

            if (_timer <= 0f)
                Destroy(gameObject);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy"))
                return;

            if (other.gameObject.TryGetComponent(out DamageOverTime existingDot))
            {
                existingDot.ResetTicks();
                return;
            }

            var dot = other.gameObject.AddComponent<DamageOverTime>();
            dot.Ticks = dotTicks;
            dot.TickTime = tickTime;
            dot.DamagePerTick = tickDamage;
        }
    }
}
