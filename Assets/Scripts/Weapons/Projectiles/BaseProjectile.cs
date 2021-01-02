using System;
using System.Linq;
using Enemies;
using Managers;
using UnityEngine;

namespace Weapons.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        protected virtual bool HurtsPlayer { get; } = false;
        protected virtual int DamageAmount { get; } = 1;
        protected virtual float ProjectileLifetime { get; } = 25f;
        
        protected Vector3 _moveDirection = Vector3.down;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (HurtsPlayer && other.CompareTag("Player"))
            {
                // We do this because we'll need to update the UI and so we can just do the player and the UI in an event.
                LevelManager.HurtPlayer(DamageAmount);
                Destroy(gameObject);
            }
            else if (!HurtsPlayer && other.CompareTag("Enemy"))
            {
                // If it's an enemy, then we're just going to damage them.
                other.GetComponent<EnemyHealth>().Damage(DamageAmount);
                Destroy(gameObject);
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Obstacle"))
                Destroy(gameObject);
        }

        public virtual void SetMovingDirection(Vector2 direction)
        {
            _moveDirection = new Vector3(direction.x, direction.y);
        }
    }
}