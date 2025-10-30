using UnityEngine;

namespace _Scripts.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class CollidableItem2D : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log(other.gameObject.name);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
        }
    }
}