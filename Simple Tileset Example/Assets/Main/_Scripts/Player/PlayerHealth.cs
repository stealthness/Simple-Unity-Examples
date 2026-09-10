using TMPro;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerHealth : MonoBehaviour
    {
        
        [SerializeField] private Color playerColor;
        
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float invulnerabilityTime = 0.5f;
            
        private int _currentHealth;
        private SpriteRenderer _sr;
        
        private bool _playerIsInvulnerable = false;
        
        
        public GameObject DeadPlayerPrefab;
        public TextMeshProUGUI playerHealthText;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _currentHealth = maxHealth;
            UpdateHealthUI();
        }

        public void TakeDamage(int damage)
        {
            if (_playerIsInvulnerable)
                return;
            
            TakeHitDamage(damage);
        }

        private void TakeHitDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                UpdateHealthUI();
                Die();
            }

            UpdateHealthUI();
            playerColor = _sr.color;
            _sr.color = Color.red + playerColor;
            Invoke(nameof(RemoveInvulnerability), invulnerabilityTime);
        }


        private void RemoveInvulnerability()
        {
            _playerIsInvulnerable = false;
            _sr.color = playerColor;
        }

        private void UpdateHealthUI()
        {
            playerHealthText.text = $"Health: {_currentHealth}/{maxHealth}";
        }

        public void Die()
        {
            _sr.color = Color.black;
            //UIManager.Instance.ShowGameLost("You lost all you health");
            Instantiate(DeadPlayerPrefab, transform.position, Quaternion.identity);
            //GameManager.Instance.GameOver();
        }

        public void SetColor(Color color)
        {
            playerColor = color;
            if (_sr.color != Color.red)
            {
                _sr.color = color;
            }
        }
    }
}