using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private void Awake()
    {
        menuPanel.SetActive(true);
    }


    private void Start()
    {
        Debug.Log("GameManager started.");
    }
    
    
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked.");
        menuPanel.SetActive(false);
        TargetSpawner.Instance.StartSpawning();
    }
}