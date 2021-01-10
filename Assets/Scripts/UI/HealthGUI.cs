using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class HealthGUI : MonoBehaviour
    {
        [SerializeField] private int healthPerHeart = 2;
        [SerializeField] private int maxHeartsPerRow = 3;
        [SerializeField] private Sprite[] images;
        [SerializeField] private float spacingX;
        [SerializeField] private float spacingY;
        [SerializeField] private Transform container;
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private Vector3 initialPosition;
        
        private int _currentHealth;
        private int _maxHealth;
        private List<GameObject> hearts = new List<GameObject>();

        private void Start()
        {
            if (LevelManager.Instance is null)
                return;
            
            _currentHealth = LevelManager.Instance.GetPlayerCurrentHealth();
            AddHearts(_currentHealth / healthPerHeart);
            UpdateHearts();
            EventBus.Instance.OnPlayerHealthChange += OnPlayerHealthChange;
        }

        private void OnDisable()
        {
            if (EventBus.Instance is null)
                return;
            
            EventBus.Instance.OnPlayerHealthChange -= OnPlayerHealthChange;
        }

        private void OnPlayerHealthChange(int newValue)
        {
            _currentHealth += newValue;
            UpdateHearts();
        }

        private void AddHearts(int amount)
        {
            var heartPosition = initialPosition;
            for (var i = 0; i < amount; i++)
            {
                var newHeart = Instantiate(heartPrefab);
                var heartRect = newHeart.GetComponent<RectTransform>();
                heartRect.SetParent(container);
                heartRect.anchoredPosition = heartPosition;
                heartRect.localScale = new Vector3(1, 1, 1);
                hearts.Add(newHeart);

                if (hearts.Count % maxHeartsPerRow == 0)
                {
                    heartPosition.x = initialPosition.x;
                    heartPosition.y += spacingY;
                }
                else
                {
                    heartPosition.x += spacingX;
                }
            }
        }
        
        private void UpdateHearts()
        {
            var currentHeart = hearts.Count;

            for (var i = 0; i < hearts.Count; i++)
            {
                var heartImage = hearts[i].GetComponent<Image>();

                if (_currentHealth < (currentHeart * healthPerHeart) - (healthPerHeart - 1))
                {
                    heartImage.sprite = images[images.Length - 1];
                }
                else
                {
                    if (_currentHealth >= currentHeart * healthPerHeart)
                    {
                        heartImage.sprite = images[0];
                    }
                    else
                    {
                        var currentHeartHealth = (healthPerHeart - (healthPerHeart * currentHeart - _currentHealth));
                        var imageIndex = healthPerHeart - currentHeartHealth;
                        heartImage.sprite = images[imageIndex];
                    }
                }
                
                currentHeart -= 1;
            }
        }
    }
}