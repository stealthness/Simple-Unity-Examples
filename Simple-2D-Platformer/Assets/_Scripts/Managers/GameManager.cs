using System;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
   
        public static GameManager Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {
            Debug.Log("GameManager Start");    
        }

    }
}
