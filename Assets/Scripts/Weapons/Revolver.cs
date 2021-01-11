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
        protected override void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            //Debug.Log("Started!");
        }

        protected override void OnAttackActionCancelled(InputAction.CallbackContext context)
        {
            //Debug.Log("Cancelled!");
        }

        protected override void OnAttackAction(InputAction.CallbackContext context)
        {
            //Debug.Log("Holding!");
        }
    }
}