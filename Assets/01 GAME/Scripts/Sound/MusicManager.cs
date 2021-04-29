using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    float volume = .5f;
    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = volume;
    }

    public void IncVol()
    {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        _audioSource.volume = volume;
    }

    public void DecVol()
    {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        _audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}