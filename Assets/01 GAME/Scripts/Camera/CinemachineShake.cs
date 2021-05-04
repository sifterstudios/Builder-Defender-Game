using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CinemachineShake : MonoBehaviour
    {
        CinemachineBasicMultiChannelPerlin _cinemachineNoise;
        float _startingIntensity;
        float _timer;
        float _timerMax;
        CinemachineVirtualCamera _virtualCamera;
        public static CinemachineShake Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachineNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        void Update()
        {
            if (_timer < _timerMax)
            {
                _timer += Time.deltaTime;
                var amplitude = Mathf.Lerp(_startingIntensity, 0f, _timer / _timerMax);
                amplitude = _cinemachineNoise.m_AmplitudeGain = amplitude;
            }
        }

        public void ShakeCamera(float intensity, float timerMax)
        {
            _timerMax = timerMax;
            _timer = 0f;
            _startingIntensity = intensity;
            _cinemachineNoise.m_AmplitudeGain = intensity;
        }
    }
}