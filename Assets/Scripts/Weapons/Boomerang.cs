using System;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class Boomerang : BaseWeapon
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private GameObject _projectile;
        private bool _hasReturned = true;

        protected override void Awake()
        {
            base.Awake();

            if (EventBus.Instance is null)
                return;

            EventBus.Instance.OnPlayerProjectileReturn += OnBoomerangReturn;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            if (EventBus.Instance is null)
                return;

            EventBus.Instance.OnPlayerProjectileReturn -= OnBoomerangReturn;
        }

        protected override void OnAttack()
        {
            if (!_hasReturned)
                return;
            
            _projectile = Instantiate(projectile, spawnPoint.position, transform.rotation);
            _projectile.GetComponent<BaseProjectile>().SetMovingDirection(Facing);
            currentCooldown = fireRate;
            spriteRenderer.enabled = _hasReturned =false;
        }

        private void OnBoomerangReturn(GameObject _)
        {
            spriteRenderer.enabled = _hasReturned =true;
        }
    }
}