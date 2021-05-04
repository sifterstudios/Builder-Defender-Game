using System;
using UnityEngine;

namespace Health
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] int healthAmountMax = 100;
        int _healthAmount;

        void Awake()
        {
            _healthAmount = healthAmountMax;
        }

        public event EventHandler OnDamaged;
        public event EventHandler OnHealed;
        public event EventHandler OnDied;
        public event EventHandler OnHealthAmountMaxChanged;

        public void Damage(int damageAmount)
        {
            _healthAmount -= damageAmount;
            _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);

            OnDamaged?.Invoke(this, EventArgs.Empty);
            if (IsDead()) OnDied?.Invoke(this, EventArgs.Empty);
        }

        public bool IsDead()
        {
            return _healthAmount == 0;
        }

        public int GetHealthAmount()
        {
            return _healthAmount;
        }

        public int GetHealthAmountMax()
        {
            return healthAmountMax;
        }

        public bool IsFullHealth()
        {
            return _healthAmount == healthAmountMax;
        }

        public float GetHealthAmountNormalized()
        {
            return (float) _healthAmount / healthAmountMax;
        }

        public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
        {
            _healthAmount = healthAmountMax;
            if (updateHealthAmount) this.healthAmountMax = _healthAmount;
            OnHealthAmountMaxChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Heal(int healAmount)
        {
            _healthAmount += healAmount;
            _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);

            OnHealed?.Invoke(this, EventArgs.Empty);
        }

        public void HealFull()
        {
            _healthAmount = healthAmountMax;
            OnHealed?.Invoke(this, EventArgs.Empty);
        }
    }
}