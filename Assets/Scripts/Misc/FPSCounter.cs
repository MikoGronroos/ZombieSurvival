using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI fpsCounterText;

    private float _frameCount = 0f;
    private float _dt = 0.0f;
    private float _fps = 0.0f;
    private float _updateRate = 4.0f;  // 4 updates per sec.

    void Update()
    {
        _frameCount++;
        _dt += Time.deltaTime;
        if (_dt > 1.0 / _updateRate)
        {
            _fps = _frameCount / _dt;
            _frameCount = 0;
            _dt -= 1.0f / _updateRate;
        }
    }

    private void LateUpdate()
    {
        fpsCounterText.text = ((int)_fps).ToString();
    }

}
