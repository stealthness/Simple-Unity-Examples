using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement2D))]
    public class PlayerController : MonoBehaviour
    {

        private PlayerMovement2D _playerMovement;


        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement2D>();
        }

        public void OnMove(InputValue value)
        {
            var inputVector = value.Get<Vector2>();
            _playerMovement.SetMoveDirection(inputVector);
        }


        public void OnJump()
        {
            Debug.Log("OnJump");
            _playerMovement.Jump();
        }
    }
}
