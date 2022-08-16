using System;
using UnityEngine;

public class Timer
{

    private Action _onCompletedCallback;
    private float _time;
    private float _currentTime;

    public Timer(float time, Action onCompletedCallback)
    {
        _time = time;
        _currentTime = time;
        _onCompletedCallback = onCompletedCallback;
    }

    public void Tick()
    {
        if (_currentTime <= 0)
        {
            _onCompletedCallback?.Invoke();
            return;
        }
        _currentTime -= 1 * Time.deltaTime;
    }

    public void ResetTimer()
    {
        _currentTime = _time;
    }

}
