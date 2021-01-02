using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class Walkman : BaseWeapon
    {
        [SerializeField] private float diskSpread = .2f;
        protected override void OnAttack(InputAction.CallbackContext _)
        {
            if (_currentCooldown > 0.0f)
                return;

            var player = LevelManager.Instance.Player.GetComponent<PlayerMovement>();
            var position = spawnPoint.position;
            var rotation = transform.rotation;
            var projectile1 = Instantiate(projectile, position, rotation);
            var projectile2 = Instantiate(projectile, position, rotation);
            var projectile3 = Instantiate(projectile, position, rotation);
            
            Vector2 facingTop = Vector2.zero, facingBottom = Vector2.zero;

            if (player.Facing == Vector2.right || player.Facing == Vector2.left)
            {
                facingTop = new Vector2(player.Facing.x, player.Facing.y + diskSpread);
                facingBottom = new Vector2(player.Facing.x, player.Facing.y - diskSpread);
            }
            else if (player.Facing == Vector2.down || player.Facing == Vector2.up)
            {
                facingTop = new Vector2(player.Facing.x + diskSpread, player.Facing.y);
                facingBottom = new Vector2(player.Facing.x  - diskSpread, player.Facing.y);
            }
            
            projectile1.GetComponent<BaseProjectile>().SetMovingDirection(facingTop);
            projectile2.GetComponent<BaseProjectile>().SetMovingDirection(player.Facing);
            projectile3.GetComponent<BaseProjectile>().SetMovingDirection(facingBottom);
        }
    }
}