using UnityEngine;
using UnityEngine.Rendering;

namespace Camera
{
    public class ChromaticAberrationEffect : MonoBehaviour
    {
        Volume _volume;
        public static ChromaticAberrationEffect Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            _volume = GetComponent<Volume>();
        }

        void Update()
        {
            if (_volume.weight > 0)
            {
                var decreaseSpeed = 1f;
                _volume.weight -= Time.deltaTime * decreaseSpeed;
            }
        }

        public void SetWeight(float weight)
        {
            _volume.weight = weight;
        }
    }
}