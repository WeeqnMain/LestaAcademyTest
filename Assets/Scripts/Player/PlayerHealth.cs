using System;
using UnityEngine;

namespace PlayerComponents
{
    public class PlayerHealth : MonoBehaviour
    {
        public event Action<int> HealthChanged;
        public event Action Dead;

        [field: SerializeField] public int MaxHealth { get; private set; }

        public int Health
        {
            get => _health;
            private set
            {
                _health = value;
                HealthChanged?.Invoke(_health);
            }
        }

        private int _health;

        private void Start()
        {
            Health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new System.ArgumentException("Damage can not be negative, given damage: "  + damage);

            if (Health <= 0) return;
            
            Health -= damage;
        
            if (Health <= 0)
            {
                Health = 0;
                Dead?.Invoke();
            }
        }
    }
}
