using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
        public TextMeshProUGUI scoreText;
        private int _score;
        
        public UnityAction<int> OnScoreChanged;

        private void OnEnable()
        {
            OnScoreChanged += AddScore;
        }

        private void OnDisable()
        {
            OnScoreChanged -= AddScore;
        }

        private void Start()
        {
            _score = 0;
            UpdateScoreText();
        }

        public void AddScore(int points)
        {
            _score += points;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = $"Score: {_score}";
        }

        public string GetScore()
        {
            return _score.ToString();
        }
}