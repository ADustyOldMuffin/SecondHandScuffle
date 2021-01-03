using Managers;
using Player;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class BaseballBat : BaseWeapon
    {
        protected override void OnAttack(InputAction.CallbackContext _)
        {
            if (_currentCooldown > 0.0f)
                return;
            
            var player = LevelManager.Instance.Player.GetComponent<PlayerMovement>();
            var proj = Instantiate(projectile, spawnPoint.position, transform.rotation);
            proj.GetComponent<BaseProjectile>().SetMovingDirection(player.Facing);
            _currentCooldown = fireRate;
        }
    }
}