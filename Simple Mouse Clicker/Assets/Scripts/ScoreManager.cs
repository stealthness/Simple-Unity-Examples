using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
        public TextMeshProUGUI scoreText;
        [SerializeField] private int score;
        
        public UnityAction<int> OnScoreChanged;
        
        private void Start()
        {
            score = 0;
            UpdateScoreText();
        }
        
        /// <summary>
        /// To ensure that GameManager constructed and available using Script Execution Order. 
        /// </summary>
        private void OnEnable()
        {
            OnScoreChanged += AddScore;
            GameManager.Instance.onStartGame.AddListener(ResetScore);
            
        }

        private void OnDisable()
        {
            OnScoreChanged -= AddScore;
            GameManager.Instance.onStartGame.RemoveListener(ResetScore);
        }

        /// <summary>
        /// Resets the score to zero and updates the score text on the UI. This method is called when the game is restarted.
        /// </summary>
        private void ResetScore()
        {
            Debug.Log("SM::Score reset.");
            score = 0;
            UpdateScoreText();
        }

        /// <summary>
        /// Adds the specified number of points to the current score and updates the score text on the UI.
        /// </summary>
        /// <param name="points">points to add</param>
        private void AddScore(int points)
        {
            score += points;
            UpdateScoreText();
        }

        /// <summary>
        /// Updates the score text on the UI to reflect the current score.
        /// </summary>
        private void UpdateScoreText()
        {
            scoreText.text = $"Score: {score}";
        }

        /// <summary>
        /// Returns the current score as a string.
        /// </summary>
        /// <returns>the score</returns>
        public string GetScore()
        {
            return score.ToString();
        }
}