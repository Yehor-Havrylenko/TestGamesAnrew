using System;

namespace Characters.Stats
{
    public interface IHeal : IReplenishable
    {
        public event Action DeathEvent;
    }
}