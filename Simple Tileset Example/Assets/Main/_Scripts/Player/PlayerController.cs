using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        private SpriteRenderer _sr;
        private AudioSource _audioSource;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveDirection;
        private PlayerHealth _playerHealth;
        [SerializeField] private AudioClip footAudioClip;
        [SerializeField] private AudioClip hurtAudioClip;
        [SerializeField] private float playerSpeed = 3f;


        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _sr = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
        }

        public void OnMove(InputValue inputValue)
        {

            _moveDirection = inputValue.Get<Vector2>().normalized;
            _rigidbody2D.linearVelocity = _moveDirection * playerSpeed;

            
        
        }
        


        private void FixedUpdate()
        {
            CheckDirection(_moveDirection.x);
            
            if (_moveDirection.magnitude > 0.1f && !_audioSource.isPlaying )
            {
                
                Debug.Log("Start : " + _moveDirection.magnitude);
                // Play walking sound
                _audioSource.PlayOneShot(footAudioClip);
            }
            if (_moveDirection.magnitude <= 0.1f && _audioSource.isPlaying && _audioSource.clip.name == footAudioClip.name)
            {
                
                Debug.Log("Stop : " + _moveDirection.magnitude);
                // Stop walking sound
                _audioSource.Stop();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Handle collision with enemy
                Debug.Log("Player collided with enemy!");
                // You can add logic here to damage the player or trigger an event
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                _audioSource.PlayOneShot(hurtAudioClip);
                if (other.gameObject.name.Contains("Bear"))
                {
                    playerHealth.TakeDamage(30); // Example damage
                }
                else if (other.gameObject.name.Contains("Turtle"))
                {
                    playerHealth.TakeDamage(20);
                }
                else if (other.gameObject.name.Contains("Crab"))
                {
                    playerHealth.TakeDamage(5);
                }
                else
                {
                    playerHealth.TakeDamage(1);
                }
                
            }
        }
        
        /// <summary>
        /// Checks the direction of the player and flips the sprite appropriately 
        /// </summary>
        /// <param name="dir">The dire of the player</param>
        private void CheckDirection(float dir)
        {
            if (dir < 0)
            {
                _sr.flipX = true;
            }

            if (dir > 0)
            {
                _sr.flipX = false;
            }
        }

        public void OnPlayerDeath()
        {
            _audioSource.Stop();
        }


    }
    
}