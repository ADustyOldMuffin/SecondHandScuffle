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

        private const int StartDamageAmount = 2;

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            return;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (HurtsPlayer || !other.CompareTag("Enemy")) return;
            
            // If it's an enemy, then we're just going to damage them.
            DamageEnemy(other.gameObject);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;
            
            DamageEnemy(other.gameObject);
        }

        private static void DamageEnemy(GameObject enemy)
        {
            var playerFacing = LevelManager.Instance.Player.GetComponent<PlayerMovement>().Facing;
            enemy.GetComponent<EnemyHealth>().Damage(StartDamageAmount, 2f, playerFacing);
        }
    }
}