using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is a simple timer manager that uses the Timer class to keep track of elapsed time and update a UI text element
/// with the current time in minutes and seconds format. It starts the timer when the game begins and updates the timer
/// text every frame.
/// </summary>
public class TimerManager : MonoBehaviour
{
    /// <summary>
    /// Event that is invoked when the countdown timer finishes. It is initialized in the Awake method and has a listener
    /// that calls the GameManager's TimerFinished method and resets the timer.
    /// </summary>
    public UnityEvent countdownFinishedEvent;
    
    [SerializeField] private TextMeshProUGUI timerText;
    
    /// <summary>
    /// The Timer instance that is used to track elapsed time. It is initialized in the Awake method with a countdown
    /// duration of 10 seconds and the countdownFinishedEvent as its event listener.
    /// </summary>
    private Timer _timer;

    /// <summary>
    /// The duration of the countdown timer in seconds. This can be set in the Unity Inspector.
    /// </summary>
    [SerializeField] private float gameCountdownDuration = 10f;
    
    [SerializeField] private TargetSpawner targetSpawner;


    /// <summary>
    /// Awake is called when the script instance is being loaded. It initializes the countdownFinishedEvent and adds a listener
    /// that calls the GameManager's TimerFinished method, clears the targets from the TargetSpawner, and resets the timer.
    /// It also initializes the Timer instance with the specified countdown duration and the countdownFinishedEvent as its event listener.
    /// </summary>
    private void Awake()
    {   
        // if we want just timer to count up, we can use the default constructor
        //_timer = new Timer(this);     
        _timer = new Timer(this, gameCountdownDuration, countdownFinishedEvent);
             
        countdownFinishedEvent = new UnityEvent();
        countdownFinishedEvent.AddListener(() =>
        {
            GameManager.Instance.TimerFinished();
            targetSpawner.ClearTargets();
            _timer.ResetTimer();
        });
    }

    private void OnEnable()
    {
        GameManager.Instance.onStartGame.AddListener(StartTimer);
    }
    
    private void OnDisable()
    {
        GameManager.Instance.onStartGame.RemoveListener(StartTimer);
    }

    /// <summary>
    /// Resets the timer by canceling any ongoing invocations of the UpdateTimer method and calling the ResetTimer
    /// method of the Timer instance.
    /// </summary>
    private void ResetTimer()
    {
        Debug.Log("TM::Reset timer finished.");
        CancelInvoke(nameof(UpdateTimer));
        _timer.ResetTimer();
    }

    /// <summary>
    /// Starts the timer by calling the StartTimer method of the Timer instance and invoking the UpdateTimer method
    /// </summary>
    private void StartTimer()
    {
        CancelInvoke(nameof(UpdateTimer));
        _timer.ResetTimer();
        _timer.StartTimer();
        InvokeRepeating(nameof(UpdateTimer), 0f, 0.1f);
    }

    /// <summary>
    /// Updates the timer text on the UI by calling the UpdateTimerText method. This method is invoked repeatedly at a
    /// fixed interval
    /// </summary>
    public void UpdateTimer()
    {
        UpdateTimerText();
    }

    /// <summary>
    /// Updates the timer text on the UI to reflect the current elapsed time of the Timer instance. It formats the elapsed time
    /// in minutes and seconds and sets the text of the timerText UI element accordingly.
    /// </summary>
    private void UpdateTimerText()
    {
        var minutes = Mathf.FloorToInt(_timer.ElapsedTime / 60f);
        var seconds = Mathf.FloorToInt(_timer.ElapsedTime % 60f);
        var text = $"{minutes:00}:{seconds:00}";
        timerText.text = text;
    }
}