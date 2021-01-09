using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
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
            EventBus.Instance.OnPlayerHealthChange -= OnPlayerHealthChange;
        }

        private void OnPlayerHealthChange(int newValue)
        {
            _currentHealth = newValue;
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
                hearts.Add(newHeart);

                if (hearts.Count % maxHeartsPerRow == 0)
                {
                    heartPosition.x = initialPosition.x;
                    heartPosition.y += initialPosition.y;
                }
                else
                {
                    heartPosition.x += initialPosition.x;
                }
            }
        }
        
        private void UpdateHearts()
        {
            var empty = false;
            var i = 0;

            foreach (var heartImage in hearts.Select(heart => heart.GetComponent<Image>()))
            {
                if (empty)
                {
                    heartImage.sprite = images[0];
                }
                else
                {
                    i += 1;
                    if (_currentHealth >= i * healthPerHeart)
                    {
                        heartImage.sprite = images[images.Length - 1];
                    }
                    else
                    {
                        var currentHeartHealth = (healthPerHeart - (healthPerHeart * i - _currentHealth));
                        var healthPerImage = Mathf.RoundToInt(healthPerHeart / images.Length);
                        var imageIndex = currentHeartHealth / healthPerImage;
                        heartImage.sprite = images[imageIndex];
                        empty = true;
                    }
                }
            }
        }
    }
}