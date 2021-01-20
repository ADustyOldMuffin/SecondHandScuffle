using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class ThrowableWeapon : BaseWeapon
    {
        [SerializeField] private float chargeRate = 80.0f;
        [SerializeField] private float minimumCharge = 10f;
        [SerializeField] private float maxCharge = 100f;
        [SerializeField] private float stuckLength = 6.0f;

        private bool _isCharging = false, _isThrown = false;
        private float _currentCharge = 0.0f, _currentThrownTime;

        protected override void Start()
        {
            if (EventBus.Instance is null)
                return;
            
            base.Start();

            EventBus.Instance.OnPlayerProjectileReturn += ReturnWeapon;
        }

        private void FixedUpdate()
        {
            if (_isCharging)
                _currentCharge += chargeRate * Time.fixedDeltaTime;

            if (!_isThrown) return;
            
            _currentThrownTime += Time.fixedDeltaTime;

            if (!(_currentThrownTime >= stuckLength)) return;
                
            _currentThrownTime = 0.0f;
        }

        protected override void OnDisable()
        {
            if (EventBus.Instance is null)
                return;
            
            base.OnDisable();

            EventBus.Instance.OnPlayerProjectileReturn -= ReturnWeapon;
        }

        protected override void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            // Only start charging if we haven't thrown the weapon
            _isCharging = !_isThrown;
        }

        protected override void OnAttackActionCancelled(InputAction.CallbackContext context)
        {
            _isCharging = false;

            if (_isThrown)
                return;
            
            ThrowWeapon(_currentCharge < minimumCharge ? minimumCharge : _currentCharge);
            _currentCharge = 0.0f;
        }

        protected override void OnAttackAction(InputAction.CallbackContext context)
        {
            
        }

        private void ThrowWeapon(float amount)
        {
            _isThrown = true;
            var proj = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
            
            // Power should be between 0 and 1 to indicate 0% -> 100%
            // You get this by amount / maxAmount
            var power = minimumCharge / maxCharge;
            if (_currentCharge > maxCharge)
                power = 1;
            else if (_currentCharge > minimumCharge)
                power = _currentCharge / maxCharge;
                
            proj.GetComponent<IThrowableProjectile>().SetThrowPower(power);
            proj.GetComponent<BaseProjectile>().SetMovingDirection(Facing);
        }

        protected virtual void ReturnWeapon(GameObject returningProjectile)
        {
            _isThrown = false;
        }
    }
}