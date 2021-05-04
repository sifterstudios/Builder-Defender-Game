using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace BD.Utilities
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] Gradient gradient;
        [SerializeField] float secondsPerDay = 10f;
        Light2D _light2D;
        float _dayTime;
        float _dayTimeSpeed;

        void Awake()
        {
            _light2D = GetComponent<Light2D>();
            _dayTimeSpeed = 1 / secondsPerDay;
        }

        void Update()
        {
            _dayTime += Time.deltaTime * _dayTimeSpeed;
            _light2D.color = gradient.Evaluate(_dayTime % 1f);

        }
    }
}