using _Scripts.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement2D : Movement2DPlatformer
    {
        [SerializeField] private float jumpCooldown = 0.5f;
        [SerializeField] private int maxJumps = 2;
        [SerializeField] private int currentJumps = 2;
        

        private void Start()
        {
            currentJumps = maxJumps;
        }
        
        
        public void SetMoveDirection(Vector2 inputVector)
        {
            moveDirection = new Vector2(inputVector.x, inputVector.y); 
        }
        
        
        public override void Jump()
        {
            if (currentJumps <= 0) return;
            
            base.Jump();
            currentJumps--;
        }

        

        private void ResetNumberOfJumps()
        {
            currentJumps = maxJumps;
        }
        
        private void Update()
        {
            if (isGrounded)
            {
                ResetNumberOfJumps();
            }
        }
    }
}
