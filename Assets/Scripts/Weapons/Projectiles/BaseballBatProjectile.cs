using System;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class BaseballBatProjectile : BaseProjectile
    {
        protected override bool HurtsPlayer { get; } = false;
        protected override int DamageAmount { get; } = 3;
        protected override float ProjectileLifetime { get; } = .25f;

        private float _aliveFor = 0.0f;
        
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            return;
        }

        private void FixedUpdate()
        {
            if(_aliveFor >= ProjectileLifetime)
                Destroy(gameObject);

            _aliveFor += Time.fixedDeltaTime;
        }
    }
}