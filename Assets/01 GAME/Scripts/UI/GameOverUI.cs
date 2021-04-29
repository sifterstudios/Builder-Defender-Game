using BD.Enemy;
using BD.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BD.UI
{
    public class GameOverUI : MonoBehaviour
    {
        public static GameOverUI Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
                GameSceneManager.Load(GameSceneManager.Scene.GameScene));
            transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
                GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene));

            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);

            transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>()
                .SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + " Waves!");
        }

        void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}