using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class NoReloadWeapon : BaseWeapon
    {
        private bool _isFiring = false;
        private void FixedUpdate()
        {
            if (currentCooldown > 0)
                currentCooldown -= Time.fixedDeltaTime;

            if (_isFiring && currentCooldown <= 0.0f)
                FireWeapon();
        }

        protected virtual void FireWeapon()
        {
            var proj = Instantiate(projectile, spawnPoint.position, transform.rotation);
            proj.GetComponent<BaseProjectile>().SetMovingDirection(Facing);
            attackSound.Play();
            EventBus.Instance.ShakeCameraRequest(shakePresetName, -Facing);
            currentCooldown = fireRate;
        }

        protected override void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            if (currentCooldown <= 0.0f)
                FireWeapon();
        }

        protected override void OnAttackActionCancelled(InputAction.CallbackContext context)
        {
            _isFiring = false;
        }

        protected override void OnAttackAction(InputAction.CallbackContext context)
        {
            _isFiring = true;
        }
    }
}