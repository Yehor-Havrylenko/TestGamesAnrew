using System;

namespace Pattern
{
    public interface IObserverListenable
    {
        void Subscribe(Action value);
        void Unsubscribe(Action value);
    }
}