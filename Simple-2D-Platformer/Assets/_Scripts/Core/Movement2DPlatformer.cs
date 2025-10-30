using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// This is a base class for 2D platformer movement mechanics.
    /// It handles basic movement and jumping functionality using Rigidbody2D.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class Movement2DPlatformer : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] protected Vector2 moveDirection = Vector2.one;
        [SerializeField] protected Vector2 jumpDirection = Vector2.up;


        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void Start()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.freezeRotation = true;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (!_rigidbody2D) return;
            
            _rigidbody2D.linearVelocity = new Vector2(moveDirection.x * moveSpeed, _rigidbody2D.linearVelocityY);
        }

        public virtual void Jump()
        {
            if (!CheckIsPlayerGrounded()) return;
            
            Debug.Log("Jump");
            _rigidbody2D.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            
        }
        
        public abstract bool CheckIsPlayerGrounded();
        
        
    }
}
