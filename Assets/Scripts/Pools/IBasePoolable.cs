using UnityEngine;

namespace Pools
{
    public interface IBasePoolable
    {
        public bool IsReturned { get; }
        public void SetPosition(Vector3 position);
        public void Play();
        public void ReturnInPool();
    }
}