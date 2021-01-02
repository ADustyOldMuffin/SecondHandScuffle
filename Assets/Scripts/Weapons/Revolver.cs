using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class Revolver : BaseWeapon
    {
        protected override void OnAttack(InputAction.CallbackContext _)
        {
            base.OnAttack(_);
            
            var player = LevelManager.Instance.Player.GetComponent<PlayerMovement>();
            var proj = Instantiate(projectile, spawnPoint.position, transform.rotation);
            proj.GetComponent<BaseProjectile>().SetMovingDirection(player.Facing);
            _currentCooldown = fireRate;
        }
    }
}