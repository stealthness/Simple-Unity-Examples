using UnityEngine;

namespace _Scripts.Core
{
    public class Movement2DPlatformer : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private Vector2 moveDirection = Vector2.one;
    
    
        private void LateUpdate()
        {
            transform.Translate(moveDirection.normalized * (moveSpeed * Time.deltaTime), Space.World);
        }
    }
}
