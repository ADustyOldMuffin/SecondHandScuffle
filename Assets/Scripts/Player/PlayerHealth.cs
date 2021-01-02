using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth;

        private int _currentHealth = 0;

        private void Awake()
        {
            //start at full health
            _currentHealth = startingHealth;
            
            if(LevelManager.Instance != null)
                LevelManager.Instance.SetPlayer(gameObject);
        }

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