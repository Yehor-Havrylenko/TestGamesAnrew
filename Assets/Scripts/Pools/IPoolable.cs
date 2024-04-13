using System;

namespace Pools
{
    public interface IPoolable<T> : IBasePoolable
    {
        public event Action<T> ReturnInPool;
    }

    public interface IPoolable<T1, T2> : IBasePoolable
    {
        public event Action<T1, T2> ReturnInPoolEvent;
    }
}