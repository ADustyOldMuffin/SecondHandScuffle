using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int enemyHealth;

        private int _currentHealth;

        public void Damage(int amount, float knockBack = 0.0f, Vector2 direction = default)
        {
            if (_currentHealth - amount <= 0)
            {
                StartCoroutine(Die());
                return;
            }

            _currentHealth -= amount;
            StartCoroutine(Damaged());
        }

        private IEnumerator Die()
        {
            // TODO Play death animation?
            Destroy(gameObject);
            yield return null;
        }

        private IEnumerator Damaged()
        {
            // TODO Maybe play a damaged animation?
            yield return null;
        }
    }
}