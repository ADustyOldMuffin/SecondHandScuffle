using System.Collections;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class Toaster //: BaseWeapon
    {
        /*private bool _isFiring = false;
        private bool _canFire = true;
        
        protected override void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            FireWeapon();
        }

        protected override void OnAttackActionCancelled(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnAttackAction(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
        
        private void FireWeapon()
        {
            if (!_canFire)
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
            canFire = false;
            yield return new WaitForSeconds(reloadTime);
            ammoCount = clipSize;
            canFire = true;
        }*/
    }
}