using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class WalkmanWeapon : NoReloadWeapon
    {
        [SerializeField] private float diskSpread;
        [SerializeField] private Sprite openSprite, closedSprite;

        private SpriteRenderer _spriteRenderer;

        protected override void Start()
        {
            base.Start();

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            base.OnAttackActionStarted(context);

            _spriteRenderer.sprite = openSprite;
        }

        protected override void OnAttackActionCancelled(InputAction.CallbackContext context)
        {
            base.OnAttackActionCancelled(context);

            _spriteRenderer.sprite = closedSprite;
        }

        protected override void FireWeapon()
        {
            if (currentCooldown > 0.0f)
                return;
            
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
            attackSound.Play();
            EventBus.Instance.ShakeCameraRequest(shakePresetName, -Facing);
            currentCooldown = fireRate;
        }
    }
}