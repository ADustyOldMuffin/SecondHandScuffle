using System;
using Enemies;
using Managers;
using Unity.Mathematics;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class BoomerangProjectile : BaseProjectile
    {
        [SerializeField] private float moveAwayForTime = 3.0f;
        [SerializeField] private float rotationSpeed = 1.0f;
        [SerializeField] private Transform projectileSprite;
        [SerializeField] private Rigidbody2D myRigidbody;
        [SerializeField] private float moveSpeed = 10f;
        
        protected override int DamageAmount { get; } = 2;
        
        private float _currentTravelTime = 0.0f;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && _currentTravelTime > moveAwayForTime)
            {
                // We do this because we'll need to update the UI and so we can just do the player and the UI in an event.
                EventBus.Instance?.WeaponProjectileReturned(gameObject);
                Destroy(gameObject);
            }
            else if (!HurtsPlayer && other.CompareTag("Enemy"))
            {
                // If it's an enemy, then we're just going to damage them.
                other.GetComponent<EnemyHealth>().TakeDamage(DamageAmount);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            return;
        }

        private void FixedUpdate()
        {
            // Spin!
            projectileSprite.Rotate(Vector3.forward, projectileSprite.rotation.z + (rotationSpeed * Time.fixedDeltaTime));

            Vector2 newPosition;
            
            if (_currentTravelTime < moveAwayForTime)
            {
                var myTransform = transform;
                newPosition = myTransform.position + _moveDirection * (moveSpeed * Time.fixedDeltaTime);
                
            }
            else
            {
                var playerTransform = LevelManager.Instance.Player.transform;
                newPosition = Vector2.MoveTowards(transform.position, playerTransform.position,
                    moveSpeed * Time.fixedDeltaTime);
            }
            
            myRigidbody.MovePosition(newPosition);

            _currentTravelTime += Time.fixedDeltaTime;
        }
    }
}