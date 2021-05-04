using Health;
using UnityEngine;
using Utilities;

namespace Building
{
    public class ArrowProjectile : MonoBehaviour
    {
        Vector3 _lastMoveDir;

        Enemy.Enemy _targetEnemy;
        float _timeToDie = 2f;

        void Update()
        {
            Vector3 moveDir;
            if (_targetEnemy != null)
            {
                moveDir = (_targetEnemy.transform.position - transform.position).normalized;
                _lastMoveDir = moveDir;
            }
            else
            {
                moveDir = _lastMoveDir;
            }

            var moveSpeed = 20f;
            transform.position += moveDir * (moveSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

            _timeToDie -= Time.deltaTime;
            if (_timeToDie < 0f) Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                // Hit an enemy!
                var damageAmount = 10;
                enemy.GetComponent<HealthSystem>().Damage(damageAmount);

                Destroy(gameObject);
            }
        }

        public static ArrowProjectile Create(Vector3 position, Enemy.Enemy enemy)
        {
            var pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");
            var arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

            var arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
            arrowProjectile.SetTarget(enemy);
            return arrowProjectile;
        }

        void SetTarget(Enemy.Enemy targetEnemy)
        {
            _targetEnemy = targetEnemy;
        }
    }
}