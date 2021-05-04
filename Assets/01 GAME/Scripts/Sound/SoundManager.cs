using FMODUnity;
using UnityEngine;

namespace BD.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public enum Sound
        {
            BuildingPlaced,
            BuildingDamaged,
            BuildingDestroyed,
            EnemyDie,
            EnemyHit,
            GameOver,
            EnemyWaveStarting
        }

        public FMODUnity.EventReference BuildingPlaced;
        public FMODUnity.EventReference BuildingDamaged;
        public FMODUnity.EventReference BuildingDestroyed;
        public FMODUnity.EventReference EnemyDie;
        public FMODUnity.EventReference EnemyHit;
        public FMODUnity.EventReference GameOver;
        public FMODUnity.EventReference EnemyWaveStarting;

        float _volume = .5f;
        public static SoundManager Instance { get; private set; }
        UnityEngine.Camera _camera;

        void Awake()
        {
            _camera = UnityEngine.Camera.main;
            Instance = this;
            // _studioEventEmitter = GetComponent<StudioEventEmitter>();
            // _audioSource = GetComponent<AudioSource>();
            _volume = PlayerPrefs.GetFloat("soundVolume", .5f);
            // _soundAudioClipDictionary = new Dictionary<Sound, StudioEventEmitter>();
            // foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            //     _soundAudioClipDictionary[sound] = Sound.BuildingPlaced.ToString();
        }

        public void PlaySound(Sound sound)
        {
            switch (sound)
            {
                case Sound.BuildingDamaged:
                    RuntimeManager.PlayOneShot(BuildingDamaged, _volume, _camera.transform.position);
                    break;
                case Sound.BuildingDestroyed:
                    RuntimeManager.PlayOneShot(BuildingDestroyed, _volume, _camera.transform.position);
                    break;
                case Sound.BuildingPlaced:
                    RuntimeManager.PlayOneShot(BuildingPlaced, _volume, _camera.transform.position);
                    break;
                case Sound.EnemyDie:
                    RuntimeManager.PlayOneShot(EnemyDie, _volume, _camera.transform.position);
                    break;
                case Sound.EnemyHit:
                    RuntimeManager.PlayOneShot(EnemyHit, _volume, _camera.transform.position);
                    break;
                case Sound.GameOver:
                    RuntimeManager.PlayOneShot(GameOver, _volume, _camera.transform.position);
                    break;
                case Sound.EnemyWaveStarting:
                    RuntimeManager.PlayOneShot(EnemyWaveStarting, _volume, _camera.transform.position);
                    break;
            }
            //_audioSource.PlayOneShot(_soundAudioClipDictionary[sound], _volume);
        }

        public void IncVol()
        {
            _volume += .1f;
            _volume = Mathf.Clamp01(_volume);
            PlayerPrefs.SetFloat("soundVolume", _volume);
        }

        public void DecVol()
        {
            _volume -= .1f;
            _volume = Mathf.Clamp01(_volume);
            PlayerPrefs.SetFloat("soundVolume", _volume);
        }

        public float GetVolume()
        {
            return _volume;
        }
    }
}