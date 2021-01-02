using System;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private void Awake()
        {
            LevelManager.OnPlayerScoreChange += OnScoreChange;
        }

        private void OnScoreChange()
        {
            scoreText.text = LevelManager.Instance.PlayerScore.ToString();
        }
    }
}