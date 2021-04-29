using System;
using System.Collections.Generic;
using UnityEngine;

namespace BD.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        public enum Sound
        {
            BuildingPlaced,
            BuildingDamaged,
            BuildingDestroyed,
            EnemyDie,
            EnemyHit,
            GameOver,
        }


        AudioSource _audioSource;
        Dictionary<Sound, AudioClip> _soundAudioClipDictionary;
        float _volume = .5f;

        void Awake()
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                _soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(Sound.BuildingPlaced.ToString());
            }
        }

        public void PlaySound(Sound sound)
        {
            _audioSource.PlayOneShot(_soundAudioClipDictionary[sound], _volume);
        }

        public void IncVol()
        {
            _volume += .1f;
            _volume = Mathf.Clamp01(_volume);
        }

        public void DecVol()
        {
            _volume -= .1f;
            _volume = Mathf.Clamp01(_volume);
        }

        public float GetVolume()
        {
            return _volume;
        }
    }
}