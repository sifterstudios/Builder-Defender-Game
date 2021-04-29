using UnityEngine;

namespace BD.Building
{
    public class Tower : MonoBehaviour
    {
        Enemy.Enemy _targetEnemy;
        Vector3 _projectileSpawnPosition;
        float _shootTimer;
        [SerializeField] float shootTimerMax;
        float _lookForTargetTimer;
        float _lookForTargetTimerMax = .2f;

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
            float targetMaxRadius = 20f;
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (Collider2D col in collider2DArray)
            {
                Enemy.Enemy enemy = col.GetComponent<Enemy.Enemy>();
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
                        {
                            // New enemy is closer!
                            _targetEnemy = enemy;
                        }
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
                if (_targetEnemy != null)
                {
                    ArrowProjectile.Create(_projectileSpawnPosition, _targetEnemy);
                }
            }
        }
    }
}