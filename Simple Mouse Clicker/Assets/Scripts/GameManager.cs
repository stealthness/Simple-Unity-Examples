using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    
    public static GameManager Instance { get; private set; }

    public UnityEvent onRestartGame;

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


    /// <summary>
    /// Called when the timer finishes counting down. It stops the game by setting the timescale to 0, displays the
    /// menu panel, and updates the title and info text with the final score.
    /// No null checks are performed, so ensure that the menuPanel and ScoreManager are properly set up before calling this method.
    /// </summary>
    public void TimerFinished()
    {
        Time.timeScale = 0f;
        menuPanel.SetActive(true);
        GetTextMeshProUGUIComponentsInMenuPanel("TitleText").text = "Time's up!";
        var message = $"Your score: {FindAnyObjectByType<ScoreManager>().GetScore()}";
        GetTextMeshProUGUIComponentsInMenuPanel("InfoText").text = message;
        onRestartGame.Invoke();
    }



    /// <summary>
    /// Gets the TextMeshProUGUI component with the specified name in the menu panel.
    /// </summary>
    /// <param name="panelName">The name of the component to find.</param>
    /// <returns>The found component, or null if not found.</returns>
    private TextMeshProUGUI GetTextMeshProUGUIComponentsInMenuPanel(string panelName)
    {
        var components = menuPanel.GetComponentsInChildren<TextMeshProUGUI>();
        return components.FirstOrDefault(component => component.name == panelName);
    }
}