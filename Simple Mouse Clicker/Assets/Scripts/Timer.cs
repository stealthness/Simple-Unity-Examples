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
    
    public bool IsCountingUp { get; set; } = true;

    public readonly UnityEvent CountdownFinishedEvent;
    
    private Coroutine _timerCoroutine;
    private readonly MonoBehaviour _timerManager;
    
    public Timer(MonoBehaviour timerManager)
    {
        _timerManager = timerManager;
    }

    public Timer(MonoBehaviour timerManager,float initialTime, UnityEvent countdownFinishedEvent)
    {
        _timerManager = timerManager;
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
    public void StopTimer()
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


}