using System;
using Enemies;
using Managers;
using Player;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class EggBeaterProjectile : BaseProjectile
    {
        protected override bool HurtsPlayer { get; } = false;
        protected override int DamageAmount { get; } = 1;

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            return;
        }
    }
}