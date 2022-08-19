using System;
using UnityEngine;

public class Timer
{

    private Action _onCompletedCallback;
    private float _time;
    private float _currentTime;

    public float CurrentTime { get { return _currentTime; } }
    public float MaxTime { get { return _time; } }

    public Timer(float time, Action onCompletedCallback)
    {
        _time = time;
        _currentTime = 0;
        _onCompletedCallback = onCompletedCallback;
    }

    public void Tick()
    {
        if (_currentTime >= _time)
        {
            _onCompletedCallback?.Invoke();
            return;
        }
        _currentTime += 1 * Time.deltaTime;
    }

    public void ResetTimer()
    {
        _currentTime = 0;
    }

}
