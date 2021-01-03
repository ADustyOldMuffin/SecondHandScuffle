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
            if(LevelManager.Instance == null)
                return;
            
            LevelManager.OnPlayerScoreChange += OnScoreChange;
            OnScoreChange();
        }

        private void OnScoreChange()
        {
            if(LevelManager.Instance == null)
                return;
            
            scoreText.text = LevelManager.Instance.PlayerScore.ToString();
        }
    }
}