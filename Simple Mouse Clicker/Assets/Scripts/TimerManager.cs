using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// This is a simple timer manager that uses the Timer class to keep track of elapsed time and update a UI text element
/// with the current time in minutes and seconds format. It starts the timer when the game begins and updates the timer
/// text every frame.
/// </summary>
public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private Timer _timer;

    private UnityEvent _countdownFinishedEvent;

    private void Awake()
    {
        _countdownFinishedEvent = new UnityEvent();
        _countdownFinishedEvent.AddListener(() => GameManager.Instance.TimerFinished());
        _timer = new Timer(this, 10, _countdownFinishedEvent);
        // if we want just timer to count up, we can use the default constructor
        //_timer = new Timer(this);
        
    }

    private void OnEnable()
    {
        GameManager.Instance.onRestartGame.AddListener(ResetTimer);
    }
    
    private void OnDisable()
    {
        GameManager.Instance.onRestartGame.RemoveListener(ResetTimer);
    }

    private void ResetTimer()
    {
        _timer.ResetTimer();
    }


    private void Start()
    {
        StartTimer();
        InvokeRepeating(nameof(UpdateTimer), 0f, 0.1f);
    }


    private void StartTimer()
    {
        _timer.StartTimer();
    }

    public void UpdateTimer()
    {
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        var minutes = Mathf.FloorToInt(_timer.ElapsedTime / 60f);
        var seconds = Mathf.FloorToInt(_timer.ElapsedTime % 60f);
        var text = $"{minutes:00}:{seconds:00}";
        timerText.text = text;
    }
}