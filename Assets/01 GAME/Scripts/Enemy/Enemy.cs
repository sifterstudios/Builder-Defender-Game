using System;
using BD.Building;
using BD.Health;
using BD.Sound;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BD.Enemy
{
    public class Enemy : MonoBehaviour
    {
        Rigidbody2D _rigidbody2D;
        Transform _targetTransform;
        float _lookForTargetTimer;
        float _lookForTargetTimerMax = .2f;
        HealthSystem _healthSystem;

        public static Enemy Create(Vector3 position)
        {
            Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
            Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

            Enemy enemy = enemyTransform.GetComponent<Enemy>();
            return enemy;
        }

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if (BuildingManager.Instance.GetHqBuilding() != null)
            {
                _targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
            }

            _healthSystem = GetComponent<HealthSystem>();
            _healthSystem.OnDamaged += (sender, args) => SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
            _healthSystem.OnDied += OnHealthSystem_OnDied;
            _lookForTargetTimer = Random.Range(0f, _lookForTargetTimerMax);
        }

        void OnHealthSystem_OnDied(object sender, EventArgs args)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
            Destroy(gameObject);
        }

        void Update()
        {
            HandleMovement();
            HandleTargeting();
        }

        void HandleTargeting()
        {
            _lookForTargetTimer -= Time.deltaTime;
            if (_lookForTargetTimer < 0f)
            {
                _lookForTargetTimer = _lookForTargetTimerMax;
                LookForTargets();
            }
        }

        void HandleMovement()
        {
            if (_targetTransform != null)
            {
                Vector3 moveDir = (_targetTransform.position - transform.position).normalized;

                float moveSpeed = 6f;
                _rigidbody2D.velocity = moveDir * moveSpeed;
            }
            else _rigidbody2D.velocity = Vector2.zero;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Building.Building building = other.gameObject.GetComponent<Building.Building>();
            if (building != null)
            {
                HealthSystem healthSystem = building.GetComponent<HealthSystem>();
                healthSystem.Damage(10);
                Destroy(gameObject);
            }
        }

        void LookForTargets()
        {
            float targetMaxRadius = 10f;
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (Collider2D collider2D in collider2DArray)
            {
                Building.Building building = collider2D.GetComponent<Building.Building>();
                if (building != null)
                {
                    //It's a building!
                    if (_targetTransform == null)
                    {
                        _targetTransform = building.transform;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, building.transform.position) <
                            Vector3.Distance(transform.position, _targetTransform.transform.position))
                        {
                            // New Building is closer!
                            _targetTransform = building.transform;
                        }
                    }
                }
            }

            if (_targetTransform == null)
            {
                // Found no targets within range;
                if (BuildingManager.Instance.GetHqBuilding() != null)
                {
                    _targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
                }
            }
        }
    }
}