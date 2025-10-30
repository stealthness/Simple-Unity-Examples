using System;
using UnityEngine;

namespace _Scripts.Core
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Movement2DPlatformer : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] protected Vector2 moveDirection = Vector2.one;


        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void Start()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody2D.linearVelocity = new Vector2(moveDirection.x * moveSpeed, _rigidbody2D.linearVelocityY);
        }
    }
}
