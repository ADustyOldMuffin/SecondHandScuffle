﻿using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth;

        private int _currentHealth = 0;

        public void DamagePlayer(int amount)
        {
            if (_currentHealth - amount <= 0)
            {
                LevelManager.PlayerDied();
            }

            _currentHealth -= amount;
            // TODO Maybe trigger an update to the UI.
        }

        public void ResetPlayerHealth()
        {
            _currentHealth = startingHealth;
        }
    }
}