using System;
using UnityEngine;

namespace Characters.Stats
{
    public class Score : MonoBehaviour, IReplenishable
    {
        private float _currentScore = 0;
        public event Action<float, float> UpdateEvent;

        void IReplenishable.Replenish(float value)
        {
            _currentScore = Math.Clamp(_currentScore + value, 0, int.MaxValue);
            UpdateEvent?.Invoke(_currentScore, 0);
        }

        void IReplenishable.Spend(float value)
        {
            _currentScore = Math.Clamp(_currentScore - value, 0, int.MaxValue);
            UpdateEvent?.Invoke(_currentScore, 0);
        }

        public void Restore()
        {
            _currentScore = 0;
            UpdateEvent?.Invoke(_currentScore, 0);
        }
    }
}