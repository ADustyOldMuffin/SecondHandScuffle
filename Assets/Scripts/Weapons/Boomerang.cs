﻿using System;
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

            LevelManager.OnPlayerWeaponReturn += OnBoomerangReturn;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            LevelManager.OnPlayerWeaponReturn -= OnBoomerangReturn;
        }

        protected override void OnAttack(InputAction.CallbackContext _)
        {
            if (!_hasReturned)
                return;
            
            var player = LevelManager.Instance.Player.GetComponent<PlayerMovement>();
            _projectile = Instantiate(projectile, spawnPoint.position, transform.rotation);
            _projectile.GetComponent<BaseProjectile>().SetMovingDirection(player.Facing);
            _currentCooldown = fireRate;
            spriteRenderer.enabled = _hasReturned =false;
        }

        private void OnBoomerangReturn()
        {
            spriteRenderer.enabled = _hasReturned =true;
        }
    }
}