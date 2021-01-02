using UnityEngine;

namespace Weapons
{
    public class Revolver : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform spawnPoint;

        public void Attack()
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}