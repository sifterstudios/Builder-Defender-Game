using BD.Sound;
using Camera;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class OptionsUI : MonoBehaviour
    {
        [SerializeField] SoundManager soundManager;
        [SerializeField] MusicManager musicManager;
        TextMeshProUGUI _musicVolumeText;
        TextMeshProUGUI _soundVolumeText;


        void Awake()
        {
            _soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
            _musicVolumeText = transform.Find("musicVolumeText").GetComponent<TextMeshProUGUI>();
            transform.Find("soundIncBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                soundManager.IncVol();
                UpdateText();
            });
            transform.Find("soundDecBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                soundManager.DecVol();
                UpdateText();
            });
            transform.Find("musicIncBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                musicManager.IncVol();
                UpdateText();
            });
            transform.Find("musicDecBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                musicManager.DecVol();
                UpdateText();
            });
            transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
                Time.timeScale = 1f;
            });
            transform.Find("edgeScrollingToggle").GetComponent<Toggle>().onValueChanged.AddListener(
                set => { CameraHandler.Instance.SetEdgeScrolling(set); });
        }

        void Start()
        {
            UpdateText();
            gameObject.SetActive(false);

            transform.Find("edgeScrollingToggle").GetComponent<Toggle>()
                .SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrolling());
        }

        void UpdateText()
        {
            _soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetVolume() * 10).ToString());
            _musicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10).ToString());
        }

        public void ToggleOptionsVisibility()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            if (gameObject.activeSelf)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }
}