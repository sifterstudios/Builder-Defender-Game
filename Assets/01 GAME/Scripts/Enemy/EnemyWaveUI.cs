using System;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] EnemyWaveManager enemyWaveManager;

    TextMeshProUGUI _waveNumberText;
    TextMeshProUGUI _waveMessageText;
    RectTransform _enemyWaveSpawnPositionIndicator;
    RectTransform _enemyClosestPositionIndicator;
    Camera _camera;

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
        _camera = Camera.main;
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
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 dirToNextSpawnPosition =
            (enemyWaveManager.GetSpawnPosition() - _camera.transform.position).normalized;
        _enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        _enemyWaveSpawnPositionIndicator.eulerAngles =
            new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition =
            Vector3.Distance(enemyWaveManager.GetSpawnPosition(), _camera.transform.position);
        _enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition >
                                                              _camera.orthographicSize * 1.5f);
    }

    void HandleEnemyClosestPositionIndicator()
    {
        float targetMaxRadius = 9999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(_camera.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;
        foreach (Collider2D col in collider2DArray)
        {
            Enemy enemy = col.GetComponent<Enemy>();
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
                    {
                        // New enemy is closer!
                        targetEnemy = enemy;
                    }
                }
            }
        }


        if (targetEnemy != null)
        {
            Vector3 dirToClosestEnemy =
                (targetEnemy.transform.position - _camera.transform.position).normalized;
            _enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;
            _enemyClosestPositionIndicator.eulerAngles =
                new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy =
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