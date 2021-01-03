using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Projectiles;

namespace Weapons
{
    public class EggBeater : BaseWeapon
    {
        private bool _attacking = false;
        private GameObject _projectile;
        
        protected override void OnAttack()
        {
            if (!_attacking)
            {
                var player = LevelManager.Instance.Player.GetComponent<PlayerMovement>();
                _projectile = Instantiate(projectile, spawnPoint.position, transform.rotation);
                _projectile.GetComponent<BaseProjectile>().SetMovingDirection(Facing);
                _attacking = true;
            }
            else
            {
                Destroy(_projectile);
                _attacking = false;
            }
        }
    }
}