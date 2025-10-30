using UnityEngine;

namespace _Scripts
{
    /// <summary>
    /// Camera follow script smooths out the camera follow of a target
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// The target to follow
        /// </summary>
        public Transform target;
        [Header("Camera Settings")]
        
        [Tooltip("The speed at which the camera follows the target")]
        [SerializeField] private float smoothSpeed = 0.125f;
        
        [Tooltip("The offset of the camera from the target")]
        [SerializeField] private Vector3 offset = new(0, 0, -10f);
        
        /// <summary>
        /// A flag to stop following the target
        /// </summary>
        private bool _stopFollowing;

        /// <summary>
        /// A smoothed out camera follow
        /// </summary>
        private void LateUpdate()
        {
            if (_stopFollowing) return;
            
            var desiredPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        
        /// <summary>
        /// Sets a new target to follow
        /// </summary>
        public void SetTarget(Transform newTarget)
        { 
            target = newTarget;
        }
        
        /// <summary>
        /// Stop following the target
        /// </summary>
        public void StopFollowing()
        {
            _stopFollowing = true;
        }
        
        /// <summary>
        /// Start following the target
        /// </summary>
        public void StartFollowing()
        {
            _stopFollowing = false;
        }
        
        /// <summary>
        /// Sets a new target to follow
        /// </summary>
        /// <param name="newTarget"> the new target to follow</param>
        public void StartFollowing(Transform newTarget)
        {
            target = newTarget;
            _stopFollowing = false;
        }
    }
}
