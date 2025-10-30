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
            GetComponent<SpriteRenderer>().color = Color.red;
            disabled = true;
            
        }
    }
}
