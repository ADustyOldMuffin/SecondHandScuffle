using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth;
        [SerializeField] private float graceTime = 2.0f;

        private int _currentHealth = 0;
        private bool _isInvincible = false;
        private float _currentGraceTime;

        private void Awake()
        {
            //start at full health
            _currentHealth = startingHealth;
            
            if(LevelManager.Instance != null)
                LevelManager.Instance.SetPlayer(gameObject);
        }

        private void FixedUpdate()
        {
            if (!_isInvincible)
                return;
            
            if (_currentGraceTime <= 0)
                _isInvincible = false;

            _currentGraceTime -= Time.fixedDeltaTime;
        }

        public void DamagePlayer(int amount)
        {
            if (_currentHealth - amount <= 0)
            {
                LevelManager.PlayerDied();
            }

            _currentHealth -= amount;
            _currentGraceTime = graceTime;
            _isInvincible = true;
        }

        public void ResetPlayerHealth()
        {
            _currentHealth = startingHealth;
        }

        public int GetPlayerHealth()
        {
            return _currentHealth;
        }

        public int GetMaxHealth()
        {
            return startingHealth;
        }
    }
}