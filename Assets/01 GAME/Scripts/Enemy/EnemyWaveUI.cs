using System;
using TMPro;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class EnemyWaveUI : MonoBehaviour
    {
        [SerializeField] EnemyWaveManager enemyWaveManager;
        UnityEngine.Camera _camera;
        RectTransform _enemyClosestPositionIndicator;
        RectTransform _enemyWaveSpawnPositionIndicator;
        TextMeshProUGUI _waveMessageText;

        TextMeshProUGUI _waveNumberText;

        void Awake()
        {
            _waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
            _waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
            _enemyWaveSpawnPositionIndicator =
                transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
            _enemyClosestPositionIndicator =
                transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();
        }

        void Start()
        {
            _camera = UnityEngine.Camera.main;
            enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
            SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
        }

        void Update()
        {
            HandleNextWaveMessage();
            HandleEnemyWaveSpawnPositionIndicator();
            HandleEnemyClosestPositionIndicator();
        }

        void HandleNextWaveMessage()
        {
            var nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
            if (nextWaveSpawnTimer <= 0f)
                SetMessageText("");
            else
                SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }

        void HandleEnemyWaveSpawnPositionIndicator()
        {
            var dirToNextSpawnPosition =
                (enemyWaveManager.GetSpawnPosition() - _camera.transform.position).normalized;
            _enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
            _enemyWaveSpawnPositionIndicator.eulerAngles =
                new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));

            var distanceToNextSpawnPosition =
                Vector3.Distance(enemyWaveManager.GetSpawnPosition(), _camera.transform.position);
            _enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition >
                                                                  _camera.orthographicSize * 1.5f);
        }

        void HandleEnemyClosestPositionIndicator()
        {
            var targetMaxRadius = 9999f;
            var collider2DArray = Physics2D.OverlapCircleAll(_camera.transform.position, targetMaxRadius);

            BD.Enemy.Enemy targetEnemy = null;
            foreach (var col in collider2DArray)
            {
                var enemy = col.GetComponent<BD.Enemy.Enemy>();
                if (enemy != null)
                {
                    //It's a enemy!
                    if (targetEnemy == null)
                    {
                        targetEnemy = enemy;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, enemy.transform.position) <
                            Vector3.Distance(transform.position, targetEnemy.transform.position))
                            // New enemy is closer!
                            targetEnemy = enemy;
                    }
                }
            }


            if (targetEnemy != null)
            {
                var dirToClosestEnemy =
                    (targetEnemy.transform.position - _camera.transform.position).normalized;
                _enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;
                _enemyClosestPositionIndicator.eulerAngles =
                    new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));

                var distanceToClosestEnemy =
                    Vector3.Distance(targetEnemy.transform.position, _camera.transform.position);
                _enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy >
                                                                    _camera.orthographicSize * 1.5f);
            }
            else
            {
                // No enemies alive!
                _enemyClosestPositionIndicator.gameObject.SetActive(false);
            }
        }

        void EnemyWaveManager_OnWaveNumberChanged(object sender, EventArgs e)
        {
            SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
        }

        void SetMessageText(string message)
        {
            _waveMessageText.SetText(message);
        }

        void SetWaveNumberText(string text)
        {
            _waveNumberText.SetText(text);
        }
    }
}