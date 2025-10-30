using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Core
{
    public class SpikeCollidable2D : CollidableItem2D
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player hit spikes!");
                other.gameObject.GetComponent<PlayerController>().PlayerDeath();
            }
        }
    }
}