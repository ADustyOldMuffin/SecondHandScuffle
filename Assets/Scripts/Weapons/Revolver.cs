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
        private bool isFiring = false;
        private bool canFire = true;

        protected override void Start()
        {
            base.Start();

            ammoCount = clipSize;
        }

        private void FixedUpdate()
        {
            if(currentCooldown > 0)
                currentCooldown -= Time.fixedDeltaTime;
 
            if(isFiring && currentCooldown <= 0.0f)
                FireWeapon();
        }

        protected override void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            FireWeapon();
        }

        protected override void OnAttackActionCancelled(InputAction.CallbackContext context)
        {
            isFiring = false;
        }

        protected override void OnAttackAction(InputAction.CallbackContext context)
        {
            Debug.Log("Holding!");
            isFiring = true;
        }

        private void FireWeapon()
        {
            if (!canFire)
                return;
            
            var proj = Instantiate(projectile, spawnPoint.position, transform.rotation);
            proj.GetComponent<BaseProjectile>().SetMovingDirection(Facing);
            EventBus.Instance.ShakeCameraRequest("PlayerGunShot_Shake", -Facing);
            currentCooldown = fireRate;
            ammoCount -= 1;

            if (ammoCount == 0)
                StartCoroutine(ReloadWeapon());

        }

        private IEnumerator ReloadWeapon()
        {
            Debug.Log("Reloading!");
            canFire = false;
            yield return new WaitForSeconds(reloadTime);
            ammoCount = clipSize;
            canFire = true;
            Debug.Log("Finished loading!");
        }
    }
}