using System;
using System.Collections;
using Animancer;
using Enemies;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class WandProjectile : BaseProjectile
    {
        protected override bool HurtsPlayer { get; } = false;
        protected override int DamageAmount { get; } = 1;
        protected override float ProjectileLifetime { get; } = 15f;

        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private Rigidbody2D myRigidbody;
        [SerializeField] private float damageDistance = 0.1f;
        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private AnimationClip targetAcquiredClip;

        private Transform _target = null;
        private float _aliveFor = 0.0f;

        private void Awake()
        {
            StartCoroutine(PlayTargetAcquiredClip());
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") &&
                Vector2.Distance(other.transform.position, transform.position) <= damageDistance)
            {
                other.GetComponent<EnemyHealth>().Damage(DamageAmount);
                Destroy(gameObject);
            }
            else if (other.CompareTag("Enemy") &&
                      Vector2.Distance(other.transform.position, transform.position) > damageDistance &&
                      _target is null)
            {
                _target = other.transform;
            }
        }

        private void FixedUpdate()
        {
            var myTransform = transform;
            Vector2 newPosition;

            if (_target == null)
            {
                newPosition = myTransform.position + _moveDirection * (moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                if (Vector2.Distance(_target.position, transform.position) <= damageDistance)
                {
                    _target.GetComponent<EnemyHealth>().Damage(DamageAmount);
                    Destroy(gameObject);
                }

                newPosition =
                    Vector2.MoveTowards(transform.position, _target.position, moveSpeed * Time.fixedDeltaTime);
            }

            myRigidbody.MovePosition(newPosition);
            
            if(_aliveFor >= ProjectileLifetime)
                Destroy(gameObject);

            _aliveFor += Time.fixedDeltaTime;
        }

        private IEnumerator PlayTargetAcquiredClip()
        {
            animancer.Play(targetAcquiredClip);
            yield return null;
        }
    }
}