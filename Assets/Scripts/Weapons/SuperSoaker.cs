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
    public class SuperSoaker : BaseWeapon
    {
        protected override void OnAttack()
        {
            if (currentCooldown > 0.0f)
                return;

            var proj = Instantiate(projectile, spawnPoint.position, transform.rotation);
            proj.GetComponent<BaseProjectile>().SetMovingDirection(Facing);
            currentCooldown = fireRate;
        }
    }
}
