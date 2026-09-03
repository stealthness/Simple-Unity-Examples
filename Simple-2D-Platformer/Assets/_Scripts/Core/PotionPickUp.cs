using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Core
{
    public class PotionPickUp : CollidableItem2D
    {
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Potion picked up!");
                other.gameObject.GetComponent<PlayerController>().Pickup("Potion");
                Destroy(gameObject); // Remove the potion from the scene
            }
        }
    }
}