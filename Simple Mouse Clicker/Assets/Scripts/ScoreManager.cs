using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
        /// <summary>
        /// Reference to the TextMeshProUGUI component that displays the score on the UI. This field is public so that
        /// it can be assigned in the Unity Inspector.
        /// </summary>
        public TextMeshProUGUI scoreText;
        
        /// <summary>
        /// The current score of the player. It is serialized so that it can be set in the Unity Inspector and is
        /// initialized to zero at the start of the game.
        /// </summary>
        [SerializeField] private int score;
        
        /// <summary>
        /// Event that is invoked when the score changes. It takes an integer parameter representing the new score value.
        /// </summary>
        public UnityAction<int> OnScoreChanged;
        
        private void Start()
        {
            score = 0;
            UpdateScoreText();
        }
        
        /// <summary>
        /// Called when the script is enabled. Subscribes to the OnScoreChanged event and the onStartGame event.
        /// </summary>
        private void OnEnable()
        {
            OnScoreChanged += AddScore;
            GameManager.Instance.onStartGame.AddListener(ResetScore);
            
        }

        /// <summary>
        /// To ensure that GameManager constructed and available using Script Execution Order.
        /// </summary>
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