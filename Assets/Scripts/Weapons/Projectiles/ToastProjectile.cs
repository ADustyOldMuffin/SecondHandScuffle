using System.Linq;
using Managers;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class ToastProjectile : NormalProjectile
    {
        protected override bool HurtsPlayer { get; } = false;
        protected override int DamageAmount { get; } = 5;
        protected override float ProjectileLifetime { get; } = 10f;

        protected void Start()
        {
            var zRotation = 0.0f;

            if (_moveDirection.x == -1)
                zRotation = 90f;
            else if (_moveDirection.x == 1)
                zRotation = 270f;
            else if (_moveDirection.y == -1)
                zRotation = 180f;
                    
            transform.rotation = Quaternion.Euler(0, 0, zRotation);
        }
    }
}