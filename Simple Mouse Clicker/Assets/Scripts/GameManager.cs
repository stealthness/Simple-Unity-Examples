using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
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


    public void TimerFinished()
    {
        Time.timeScale = 0f;
        menuPanel.SetActive(true);
        var timerText = menuPanel.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        foreach (var text in timerText)
        {
            if (text.name == "TitleText")
            {
                text.text = "Time's up!";
            }
            if (text.name == "InfoText")
            {
                var scoreManager = FindAnyObjectByType<ScoreManager>();
                text.text = $"Your score: {scoreManager.GetScore()}";
            }
        }
    }
}