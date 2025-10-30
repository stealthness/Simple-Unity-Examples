using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement2D : Movement2DPlatformer
    {
        public void SetMoveDirection(Vector2 inputVector)
        {
            moveDirection = new Vector2(inputVector.x, inputVector.y);
        }
    }
}
