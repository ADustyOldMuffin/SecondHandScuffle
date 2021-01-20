using System;
using Animancer;
using Enemies;
using Managers;
using Unity.Mathematics;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class BoomerangProjectile : BaseProjectile, IThrowableProjectile
    {
        [SerializeField] private float moveAwayForTime = 5.0f;
        [SerializeField] private float maxReturnTime = 12f;
        [SerializeField] private float rotationSpeed = 1.0f;
        [SerializeField] private Transform projectileSprite;
        [SerializeField] private Rigidbody2D myRigidbody;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private AnimationClip spin;

        private void Start()
        {
            _animancer.Play(spin);
        }

        protected override int DamageAmount { get; } = 2;
        
        private float _travelAwayTime = 0.0f;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && _travelAwayTime <= 0.0f)
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
            Vector2 newPosition;
            
            // Traveling away from the player
            if (_travelAwayTime > 0.0f)
            {
                var myTransform = transform;
                newPosition = myTransform.position + _moveDirection * (moveSpeed * Time.fixedDeltaTime);
                
            }
            // Traveling to the player
            else
            {
                var playerTransform = LevelManager.Instance.Player.transform;
                newPosition = Vector2.MoveTowards(transform.position, playerTransform.position,
                    moveSpeed * Time.fixedDeltaTime);
            }
            
            myRigidbody.MovePosition(newPosition);

            _travelAwayTime -= Time.fixedDeltaTime;
            
            // We've spent to long and the player will have already had us "return", so we're probably stuck
            if(_travelAwayTime < -(maxReturnTime))
                Destroy(gameObject);
        }

        public void SetThrowPower(float amount)
        {
            Debug.Log(amount);
            _travelAwayTime = moveAwayForTime * amount;
            
        }
    }
}