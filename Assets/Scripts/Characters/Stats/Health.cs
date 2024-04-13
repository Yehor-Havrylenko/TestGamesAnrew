using System;
using UnityEngine;

namespace Characters.Stats
{
    public class Health : MonoBehaviour, IHeal
    {
        public event Action<float, float> UpdateEvent;
        public event Action DeathEvent;
        private float _maxHealth;
        private float _currentHealth;
        public void Replenish(float value)
        {
            _currentHealth = Math.Clamp(_currentHealth + value, 0, _maxHealth);
            UpdateEvent?.Invoke(_currentHealth, _maxHealth);
        }

        public void Spend(float value)
        {
            _currentHealth = Math.Clamp(_currentHealth - value, 0, _maxHealth);
            UpdateEvent?.Invoke(_currentHealth, _maxHealth);
            if(_currentHealth == 0) DeathEvent?.Invoke();
        }

        public void Restore() { }

        public void SetMaxHealth(float value)
        {
            _maxHealth = value;
            _currentHealth = _maxHealth;
            UpdateEvent?.Invoke(_currentHealth, _maxHealth);
        }
    }
}