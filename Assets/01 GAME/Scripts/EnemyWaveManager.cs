using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public event EventHandler OnWaveNumberChanged;

    enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    [SerializeField] List<Transform> spawnPositionTransformList;
    [SerializeField] Transform nextWaveSpawnPositionTransform;

    State _state;
    int _waveNumber;
    float _nextWaveSpawnTimer;
    float _nextEnemySpawnTimer;
    int _remainingEnemySpawnAmount;
    Vector3 _spawnPosition;

    void Start()
    {
        _state = State.WaitingToSpawnNextWave;
        _spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)]
            .position;
        nextWaveSpawnPositionTransform.position = _spawnPosition;
        _nextWaveSpawnTimer = 3f;
    }

    void Update()
    {
        switch (_state)
        {
            case State.WaitingToSpawnNextWave:
            {
                _nextWaveSpawnTimer -= Time.deltaTime;
                if (_nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }

                break;
            }
            case State.SpawningWave:
            {
                if (_remainingEnemySpawnAmount > 0)
                {
                    _nextEnemySpawnTimer -= Time.deltaTime;
                    if (_nextEnemySpawnTimer < 0f)
                    {
                        _nextEnemySpawnTimer = UnityEngine.Random.Range(0f, .2f);
                        Enemy.Create(_spawnPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        _remainingEnemySpawnAmount--;

                        if (_remainingEnemySpawnAmount <= 0)
                        {
                            _state = State.WaitingToSpawnNextWave;
                            _spawnPosition =
                                spawnPositionTransformList[
                                        UnityEngine.Random.Range(0, spawnPositionTransformList.Count)]
                                    .position;
                            nextWaveSpawnPositionTransform.position = _spawnPosition;
                            _nextWaveSpawnTimer = 10f;
                        }
                    }
                }

                break;
            }
        }
    }

    void SpawnWave()
    {
        _remainingEnemySpawnAmount = 5 + 3 * _waveNumber;
        _state = State.SpawningWave;
        _waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return _waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return _nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return _spawnPosition;
    }
}