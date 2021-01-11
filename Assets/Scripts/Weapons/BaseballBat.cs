using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class BaseballBat : BaseWeapon
    {
        protected override void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnAttackActionCancelled(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnAttackAction(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}