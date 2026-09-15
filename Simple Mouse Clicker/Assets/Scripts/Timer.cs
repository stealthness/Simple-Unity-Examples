using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A simple timer class that tracks elapsed time and provides methods to start and stop the timer. It uses a coroutine
/// to update the elapsed time every frame while the timer is running.
/// </summary>
public class Timer
{
    public float ElapsedTime{ get ; private set; }
    public bool IsRunning { get ; private set; }

    private bool IsCountingUp { get; set; } = true; // set 

    public readonly UnityEvent CountdownFinishedEvent;
    
    private Coroutine _timerCoroutine;
    private readonly MonoBehaviour _timerManager;
    private readonly float _initialCountdownTime; // store initial countdown duration
    
    /// <summary>
    /// Initializes a new instance of the Timer class with a reference to a MonoBehaviour that will manage the timer's coroutine.
    /// </summary>
    /// <param name="timerManager">The MonoBehaviour that will manage the timer's coroutine.</param>
    public Timer(MonoBehaviour timerManager)
    {
        _timerManager = timerManager;
        _initialCountdownTime = 0f;
    }

    /// <summary>
    /// Initializes a new instance of the Timer class with a reference to a MonoBehaviour that will manage the timer's coroutine,
    /// an initial countdown time, and a UnityEvent that will be invoked when the countdown finishes
    /// </summary>
    /// <param name="timerManager">The MonoBehaviour that will manage the timer's coroutine.</param>
    /// <param name="initialTime">The initial countdown time.</param>
    /// <param name="countdownFinishedEvent">The UnityEvent that will be invoked when the countdown finishes.</param>
    public Timer(MonoBehaviour timerManager,float initialTime, UnityEvent countdownFinishedEvent)
    {
        _timerManager = timerManager;
        _initialCountdownTime = initialTime;
        ElapsedTime = initialTime;
        IsCountingUp = false;
        CountdownFinishedEvent = countdownFinishedEvent;
    }



    /// <summary>
    /// Starts the timer by initiating a coroutine that updates the elapsed time every frame.
    /// If the timer is already running, it does nothing.
    /// </summary>
    public void StartTimer()
    {
        if (IsRunning) return;
        
        IsRunning = true;
        _timerCoroutine = _timerManager.StartCoroutine(RunTimer());

    }
    
    /// <summary>
    /// Starts the timer in countdown mode, counting down from the specified countdownTime to zero.
    /// If the timer is already running, it does nothing.
    /// </summary>
    /// <param name="countdownTime"></param>
    public void StartCountdown(float countdownTime)
    {
        if (IsRunning) return;
        
        IsRunning = true;
        ElapsedTime = countdownTime;
        IsCountingUp = false;
        _timerCoroutine = _timerManager.StartCoroutine(RunTimer());
    }
    

    /// <summary>
    /// Stops the timer by stopping the coroutine that updates the elapsed time.
    /// </summary>
    private void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            _timerManager.StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
        
        IsRunning = false;
    }

    /// <summary>
    /// A coroutine that updates the elapsed time every frame while the timer is running.
    /// </summary>
    /// <returns>An IEnumerator instance</returns>
    private IEnumerator RunTimer()
    {
        while (IsRunning)
        {
            if (IsCountingUp)
            {
                ElapsedTime += Time.deltaTime;
            }
            else
            {
                ElapsedTime -= Time.deltaTime;
                if (ElapsedTime <= 0)
                {
                    ElapsedTime = 0;
                    StopTimer();
                    CountdownFinishedEvent?.Invoke();
                }
            }
            yield return null;
        }
        _timerCoroutine = null;
    }

    /// <summary>
    /// Resets the timer to its initial state. If the timer is counting up, it resets the elapsed time to zero.
    /// If the timer is counting down, it resets the elapsed time to the initial countdown time
    /// </summary>
    public void ResetTimer()
    {
        if (IsCountingUp)
        {
            ElapsedTime = 0;
        }
        else
        {
            // Reset to the initial countdown time so the countdown restarts correctly
            ElapsedTime = _initialCountdownTime;
        }
        StopTimer();
    }
}