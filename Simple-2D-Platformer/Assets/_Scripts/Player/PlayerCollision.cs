using System;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCollision : MonoBehaviour
    {
        private Collider2D _collider2D;
        private PlayerMovement2D _playerMovement;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _playerMovement = GetComponent<PlayerMovement2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _playerMovement.IsGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _playerMovement.IsGrounded = false;
            }
        }
        
        
        
    }
}