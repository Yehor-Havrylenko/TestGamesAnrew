using System;

namespace Characters.Stats
{
    public interface IReplenishable
    {
        public event Action<float, float> UpdateEvent;
        void Replenish(float value);
        void Spend(float value);
        void Restore();
    }
}