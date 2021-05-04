using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace BD.Sound
{
    public class MusicManager : MonoBehaviour
    {
        public EventReference musicReference;
        EventInstance _musicInstance;
        float _volume = .5f;

        void Start()
        {
            _volume = PlayerPrefs.GetFloat("musicVolume", .5f);
            _musicInstance = RuntimeManager.CreateInstance(musicReference);
            _musicInstance.setVolume(_volume);
            _musicInstance.start();
        }

        void OnDestroy()
        {
            StopAllPlayerEvents();
        }

        void StopAllPlayerEvents()
        {
            Bus masterBus = RuntimeManager.GetBus("bus:/MusicLvl");
            masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        public void IncVol()
        {
            _volume += .1f;
            _volume = Mathf.Clamp01(_volume);
            _musicInstance.setVolume(_volume);
            PlayerPrefs.SetFloat("musicVolume", _volume);
        }

        public void DecVol()
        {
            _volume -= .1f;
            _volume = Mathf.Clamp01(_volume);
            _musicInstance.setVolume(_volume);
            PlayerPrefs.SetFloat("musicVolume", _volume);
        }

        public float GetVolume()
        {
            return _volume;
        }
    }
}