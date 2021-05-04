using UnityEngine;

namespace Building
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] float shootTimerMax;
        float _lookForTargetTimer;
        readonly float _lookForTargetTimerMax = .2f;
        Vector3 _projectileSpawnPosition;
        float _shootTimer;
        Enemy.Enemy _targetEnemy;

        void Awake()
        {
            _projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
        }

        void Update()
        {
            HandleTargeting();
            HandleShooting();
        }

        void LookForTargets()
        {
            var targetMaxRadius = 20f;
            var collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (var col in collider2DArray)
            {
                var enemy = col.GetComponent<Enemy.Enemy>();
                if (enemy != null)
                {
                    //It's a enemy!
                    if (_targetEnemy == null)
                    {
                        _targetEnemy = enemy;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, enemy.transform.position) <
                            Vector3.Distance(transform.position, _targetEnemy.transform.position))
                            // New enemy is closer!
                            _targetEnemy = enemy;
                    }
                }
            }
        }

        void HandleTargeting()
        {
            _lookForTargetTimer -= Time.deltaTime;
            if (_lookForTargetTimer < 0f)
            {
                _lookForTargetTimer += _lookForTargetTimerMax;
                LookForTargets();
            }
        }

        void HandleShooting()
        {
            _shootTimer -= Time.deltaTime;
            if (_shootTimer <= 0f)
            {
                _shootTimer += shootTimerMax;
                if (_targetEnemy != null) ArrowProjectile.Create(_projectileSpawnPosition, _targetEnemy);
            }
        }
    }
}