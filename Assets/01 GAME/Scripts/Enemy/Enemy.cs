using System;
using BD.Sound;
using BD.Utilities;
using Building;
using Camera;
using Health;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BD.Enemy
{
    public class Enemy : MonoBehaviour
    {
        HealthSystem _healthSystem;
        float _lookForTargetTimer;
        readonly float _lookForTargetTimerMax = .2f;
        Rigidbody2D _rigidbody2D;
        Transform _targetTransform;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if (BuildingManager.Instance.GetHqBuilding() != null)
                _targetTransform = BuildingManager.Instance.GetHqBuilding().transform;

            _healthSystem = GetComponent<HealthSystem>();
            _healthSystem.OnDamaged += HealthSystem_OnDamaged;
            _healthSystem.OnDied += OnHealthSystem_OnDied;
            _lookForTargetTimer = Random.Range(0f, _lookForTargetTimerMax);
        }

        void Update()
        {
            HandleMovement();
            HandleTargeting();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            var building = other.gameObject.GetComponent<Building.Building>();
            if (building != null)
            {
                var healthSystemCollider = building.GetComponent<HealthSystem>();
                healthSystemCollider.Damage(10);
                _healthSystem.Damage(999);
            }
        }

        public static Enemy Create(Vector3 position)
        {
            var enemyTransform = Instantiate(GameAssets.Instance.pfEnemy, position, Quaternion.identity);

            var enemy = enemyTransform.GetComponent<Enemy>();
            return enemy;
        }

        void HealthSystem_OnDamaged(object sender, EventArgs args)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
            CinemachineShake.Instance.ShakeCamera(3f, .05f);
            ChromaticAberrationEffect.Instance.SetWeight(.5f);
        }

        void OnHealthSystem_OnDied(object sender, EventArgs args)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
            CinemachineShake.Instance.ShakeCamera(4f, .1f);
            ChromaticAberrationEffect.Instance.SetWeight(.5f);
            Instantiate(GameAssets.Instance.pfEnemyDieParticles,
                transform.position, quaternion.identity);
            Destroy(gameObject);
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
                var moveDir = (_targetTransform.position - transform.position).normalized;

                var moveSpeed = 4f;
                _rigidbody2D.velocity = moveDir * moveSpeed;
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }

        void LookForTargets()
        {
            var targetMaxRadius = 10f;
            var collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (var col in collider2DArray)
            {
                var building = col.GetComponent<Building.Building>();
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
                            // New Building is closer!
                            _targetTransform = building.transform;
                    }
                }
            }

            if (_targetTransform == null)
                // Found no targets within range;
                if (BuildingManager.Instance.GetHqBuilding() != null)
                    _targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
        }
    }
}