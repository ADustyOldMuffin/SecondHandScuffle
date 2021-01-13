using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class ReloadableWeapon : BaseWeapon
    {
        private bool _isFiring = false;
        private bool _canFire = true;

        protected override void Start()
        {
            base.Start();

            ammoCount = clipSize;
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
            EventBus.Instance.ShakeCameraRequest(shakePresetName, -Facing);
            EventBus.Instance.PlayerPushRequest(-Facing, knockBackDistance);
            currentCooldown = fireRate;
            ammoCount -= 1;

            if (ammoCount == 0)
                StartCoroutine(ReloadWeapon());

        }

        private IEnumerator ReloadWeapon()
        {
            _canFire = false;
            yield return new WaitForSeconds(reloadTime);
            ammoCount = clipSize;
            _canFire = true;
        }
    }
}