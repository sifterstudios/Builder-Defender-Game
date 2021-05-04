using System;
using System.Collections.Generic;
using BD.Sound;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] List<Transform> spawnPositionTransformList;
        [SerializeField] Transform nextWaveSpawnPositionTransform;
        float _nextEnemySpawnTimer;
        float _nextWaveSpawnTimer;
        int _remainingEnemySpawnAmount;
        Vector3 _spawnPosition;

        State _state;
        int _waveNumber;
        public static EnemyWaveManager Instance { get; private set; }


        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _state = State.WaitingToSpawnNextWave;
            _spawnPosition = spawnPositionTransformList[Random.Range(0, spawnPositionTransformList.Count)]
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
                    if (_nextWaveSpawnTimer < 0f) SpawnWave();

                    break;
                }
                case State.SpawningWave:
                {
                    if (_remainingEnemySpawnAmount > 0)
                    {
                        _nextEnemySpawnTimer -= Time.deltaTime;
                        if (_nextEnemySpawnTimer < 0f)
                        {
                            _nextEnemySpawnTimer = Random.Range(0f, .2f);
                            BD.Enemy.Enemy.Create(_spawnPosition + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
                            _remainingEnemySpawnAmount--;

                            if (_remainingEnemySpawnAmount <= 0)
                            {
                                _state = State.WaitingToSpawnNextWave;
                                _spawnPosition =
                                    spawnPositionTransformList[
                                            Random.Range(0, spawnPositionTransformList.Count)]
                                        .position;
                                nextWaveSpawnPositionTransform.position = _spawnPosition;
                                _nextWaveSpawnTimer = 15f;
                            }
                        }
                    }

                    break;
                }
            }
        }

        public event EventHandler OnWaveNumberChanged;

        void SpawnWave()
        {
            _remainingEnemySpawnAmount = 5 + 3 * _waveNumber;
            _state = State.SpawningWave;
            _waveNumber++;
            OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
            SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyWaveStarting);
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

        enum State
        {
            WaitingToSpawnNextWave,
            SpawningWave
        }
    }
}