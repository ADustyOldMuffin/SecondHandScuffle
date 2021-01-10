using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                //Debug.Log("I'm dying");
                StartCoroutine(Die());
            }
        }

        public void ChangeHealth(int amount)
        {
            if (amount < 0)
            {
                // We were damaged
                StartCoroutine(Damaged());
            }

            _currentHealth += amount;
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