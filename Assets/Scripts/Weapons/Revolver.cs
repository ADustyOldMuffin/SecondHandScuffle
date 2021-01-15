using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Weapons.Projectiles;

namespace Weapons
{
    public class Revolver : BaseWeapon
    {
        private bool _isFiring = false;
        private bool _canFire = true;

        protected override void Start()
        {
            base.Start();

            AmmoCount = clipSize;
        }

        private void FixedUpdate()
        {
            if(currentCooldown > 0)
                currentCooldown -= Time.fixedDeltaTime;
 
            if(_isFiring && currentCooldown <= 0.0f)
                FireWeapon();
        }

        protected override void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            FireWeapon();
        }

        protected override void OnAttackActionCancelled(InputAction.CallbackContext context)
        {
            _isFiring = false;
        }

        protected override void OnAttackAction(InputAction.CallbackContext context)
        {
            Debug.Log("Holding!");
            _isFiring = true;
        }

        private void FireWeapon()
        {
            if (!_canFire)
                return;
            
            var proj = Instantiate(projectile, spawnPoint.position, transform.rotation);
            proj.GetComponent<BaseProjectile>().SetMovingDirection(Facing);
            EventBus.Instance.ShakeCameraRequest("PlayerGunShot_Shake", -Facing);
            currentCooldown = fireRate;
            AmmoCount -= 1;

            if (AmmoCount == 0)
                StartCoroutine(ReloadWeapon());

        }

        private IEnumerator ReloadWeapon()
        {
            _canFire = false;
            yield return new WaitForSeconds(reloadTime);
            AmmoCount = clipSize;
            _canFire = true;
        }
    }
}