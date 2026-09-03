using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;

    private void Awake()
    {
        MenuPanel.SetActive(true);
    }


    private void Start()
    {
        Debug.Log("GameManager started.");
    }
    
    
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked.");
        MenuPanel.SetActive(false);
        TargetSpawner.Instance.StartSpawning();
    }
}