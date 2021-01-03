using Managers;
using Player;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class OldWand : BaseWeapon
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