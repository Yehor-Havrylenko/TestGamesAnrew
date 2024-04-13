using System;

namespace Characters.Stats
{
    public interface IScorable : IReplenishable
    {
        event Action<int> UpdateScoreEvent;
    }
}