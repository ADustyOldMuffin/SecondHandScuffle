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

        protected override void OnAttackAction(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

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

        private void OnBoomerangReturn(GameObject _)
        {
            spriteRenderer.enabled = _hasReturned =true;
        }
    }
}