using System;
using Managers;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortRelativeToPlayer : MonoBehaviour
{
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
                if (LevelManager.Instance is null)
                        return;

                var playerY = LevelManager.Instance.Player.transform.position.y;

                if (playerY - transform.position.y > 0)
                {
                        _spriteRenderer.sortingOrder = 1;
                }
                else
                {
                        _spriteRenderer.sortingOrder = -1;
                }
        }
}