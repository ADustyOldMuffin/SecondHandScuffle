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
            if (currentCooldown > 0.0f)
                return;

            var player = LevelManager.Instance.Player.GetComponent<PlayerMovement>();
            var position = spawnPoint.position;
            var rotation = transform.rotation;
            var projectile1 = Instantiate(projectile, position, rotation);
            var projectile2 = Instantiate(projectile, position, rotation);
            var projectile3 = Instantiate(projectile, position, rotation);
            
            Vector2 facingTop = Vector2.zero, facingBottom = Vector2.zero;

            if (Facing == Vector2.right || Facing == Vector2.left)
            {
                facingTop = new Vector2(Facing.x, Facing.y + diskSpread);
                facingBottom = new Vector2(Facing.x, Facing.y - diskSpread);
            }
            else if (Facing == Vector2.down || Facing == Vector2.up)
            {
                facingTop = new Vector2(Facing.x + diskSpread, Facing.y);
                facingBottom = new Vector2(Facing.x  - diskSpread, Facing.y);
            }
            
            projectile1.GetComponent<BaseProjectile>().SetMovingDirection(facingTop);
            projectile2.GetComponent<BaseProjectile>().SetMovingDirection(Facing);
            projectile3.GetComponent<BaseProjectile>().SetMovingDirection(facingBottom);
            currentCooldown = fireRate;
        }
    }
}