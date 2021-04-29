using UnityEngine;

namespace BD.Sound
{
    public class MusicManager : MonoBehaviour
    {
        float _volume = .5f;
        AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = _volume;
        }

        public void IncVol()
        {
            _volume += .1f;
            _volume = Mathf.Clamp01(_volume);
            _audioSource.volume = _volume;
        }

        public void DecVol()
        {
            _volume -= .1f;
            _volume = Mathf.Clamp01(_volume);
            _audioSource.volume = _volume;
        }

        public float GetVolume()
        {
            return _volume;
        }
    }
}