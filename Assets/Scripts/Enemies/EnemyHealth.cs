using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        public Animator myAnimator;

        [SerializeField] private int _currentHealth = 5;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (amount > 0)
            {
                // We were damaged
                StartCoroutine(Damaged());
            }

            _currentHealth -= amount;
        }

        private IEnumerator Damaged()
        {
            // TODO Maybe play a damaged animation?
            myAnimator.SetBool("hasBeenHit", true);
            //myMovement.KnockBack();
            yield return null;
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }


    }
}