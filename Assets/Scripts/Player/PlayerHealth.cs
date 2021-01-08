using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth;
        [SerializeField] private float graceTime = 2.0f;
        [SerializeField] private bool killPlayer = false;

        private int _currentHealth = 0;
        private bool _isInvincible = false;
        private float _currentGraceTime;
        private bool _isDead = false;

        private void Awake()
        {
            //start at full health
            _currentHealth = startingHealth;
            
            if(LevelManager.Instance != null)
                LevelManager.Instance.SetPlayer(gameObject);
        }

        private void FixedUpdate()
        {
            if(killPlayer && !_isDead)
                KillPlayer();
            
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
                KillPlayer();
            }

            _currentHealth -= amount;
            _currentGraceTime = graceTime;
            _isInvincible = true;
        }

        private void KillPlayer()
        {
            if (_isDead)
                return;
            
            EventBus.Instance?.PlayerDied();
            _isDead = true;
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