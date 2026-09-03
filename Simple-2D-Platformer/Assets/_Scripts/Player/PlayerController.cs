using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement2D))]
    public class PlayerController : MonoBehaviour
    {

        private PlayerMovement2D _playerMovement;
        
        [SerializeField] private bool disabled = false;


        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement2D>();
        }

        private void Update()
        {
            Debug.Log("PlayerController Update - Disabled: " + disabled);
            if (disabled)
            {
                _playerMovement.Stop();
            }
        }

        public void OnMove(InputValue value)
        {
            if(disabled) return;
            
            var inputVector = value.Get<Vector2>();
            _playerMovement.SetMoveDirection(inputVector);
        }


        public void OnJump()
        {
            if(disabled) return;
            
            Debug.Log("OnJump");
            _playerMovement.Jump();
        }

        public void PlayerDeath()
        {
            Debug.Log("PlayerController: Player Died!");
            _playerMovement.disabled = true;
            _playerMovement.Stop();
            GetComponent<SpriteRenderer>().color = Color.red;
            disabled = true;
            
        }

        public void Pickup(string item)
        {
            if (disabled) return;

            if (item == "Potion")
            {
                Debug.Log("Picked up: " + item);
                // Implement potion pickup logic here (e.g., increase health, add to inventory, etc.)
                GetComponent<SpriteRenderer>().color = Color.green;
                Invoke(nameof(ReturnToNormalColor), 2f);
            }
        }

        private void ReturnToNormalColor()
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
