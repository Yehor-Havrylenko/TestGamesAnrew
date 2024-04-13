using System;
using UnityEngine;

namespace Characters.Stats
{
    public class Mana : MonoBehaviour, IReplenishable
    {
        private float _maxMana;
        private float _currentMana;
        public event Action<float, float> UpdateEvent;
        public event Action ManaReadyEvent;

        public void SetMaxMana(float value)
        {
            _maxMana = value;
            _currentMana = _maxMana;
            UpdateEvent?.Invoke(_currentMana, _maxMana);
        }

        public void Restore()
        {
            _currentMana = 0;
            UpdateEvent?.Invoke(_currentMana, _maxMana);
        }

        public void Replenish(float value)
        {
            _currentMana = Mathf.Clamp(_currentMana + value, 0, _maxMana);
            UpdateEvent?.Invoke(_currentMana, _maxMana);
            if (_currentMana == _maxMana) ManaReadyEvent?.Invoke();
        }

        public void Spend(float value)
        {
            _currentMana = Mathf.Clamp(_currentMana - value, 0, _maxMana);
            UpdateEvent?.Invoke(_currentMana, _maxMana);
        }
    }
}