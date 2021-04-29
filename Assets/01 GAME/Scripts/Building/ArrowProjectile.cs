using BD.Health;
using BD.Utilities;
using UnityEngine;

namespace BD.Building
{
    public class ArrowProjectile : MonoBehaviour
    {
        public static ArrowProjectile Create(Vector3 position, Enemy.Enemy enemy)
        {
            Transform pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");
            Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

            ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
            arrowProjectile.SetTarget(enemy);
            return arrowProjectile;
        }

        Enemy.Enemy _targetEnemy;
        Vector3 _lastMoveDir;
        float _timeToDie = 2f;
        void Update()
        {
            Vector3 moveDir;
            if (_targetEnemy!=null)
            {
                moveDir = (_targetEnemy.transform.position - transform.position).normalized;
                _lastMoveDir = moveDir;
            }
            else
            {
                moveDir = _lastMoveDir;
            }

            float moveSpeed = 20f;
            transform.position += moveDir * (moveSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

            _timeToDie -= Time.deltaTime;
            if (_timeToDie < 0f)
            {
                Destroy(gameObject);
            }
        }

        void SetTarget(Enemy.Enemy targetEnemy)
        {
            _targetEnemy = targetEnemy;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Enemy.Enemy enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                // Hit an enemy!
                int damageAmount = 10;
                enemy.GetComponent<HealthSystem>().Damage(damageAmount);

                Destroy(gameObject);
            }
        }
    }
}