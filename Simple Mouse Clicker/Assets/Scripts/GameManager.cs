using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Main GameManage class. Has two events onRestartGame and onStartGame, and in this simple example holds reference to
/// UI panel and StartButton
/// </summary>
public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private ScoreManager scoreManager;
    
    /// <summary>
    /// Singleton instance of the GameManager class.
    /// This property allows other classes to access the GameManager instance easily.
    /// </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Event that is invoked when the game starts.
    /// Other classes can subscribe to this event to perform actions when the game begins.
    /// </summary>
    public UnityEvent onStartGame;

    private void Awake()
    {
        Instance = this;
        if (onStartGame == null)
        {
            onStartGame = new UnityEvent();
        }

        menuPanel.SetActive(true);
        if (!scoreManager)
        {
            scoreManager = FindAnyObjectByType<ScoreManager>();
        }
    }

    /// <summary>
    /// Start the game application with timescale at 0
    /// </summary>
    private void Start()
    {
        Time.timeScale = 0f;
    }
    
    
    /// <summary>
    /// This method handles the click event of the Start button. It logs a message, starts the game by setting the
    /// timescale to 1, hides the menu panel, and invokes the onStartGame event.
    /// </summary>
    public void OnStartButtonClick()
    {
        StartGame();
    }

    /// <summary>
    /// Starts the game by setting the timescale to 1, hiding the menu panel, and invoking the onStartGame event.
    /// </summary>
    private void StartGame()
    {
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
        onStartGame?.Invoke();
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

        UpdateTextInMenuPanel("TitleText", "Time's up!");
        var message = $"Your score: {scoreManager.GetScore()}";
        UpdateTextInMenuPanel("InfoText", message);
    }

    /// <summary>
    /// Updates the text of a TextMeshProUGUI component with the specified name in the menu panel.
    /// </summary>
    /// <param name="componentName">The name of the component to update.</param>
    /// <param name="newText">The new text to set.</param>
    private void UpdateTextInMenuPanel(string componentName, string newText)
    {
        var textComponent = GetTextMeshProUGUIComponentsInMenuPanel(componentName);
        if (textComponent)
        {
            textComponent.text = newText;
        }
        else
        {
            Debug.LogWarning($"TextMeshProUGUI component with name '{componentName}' not found in the menu panel.");
        }
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