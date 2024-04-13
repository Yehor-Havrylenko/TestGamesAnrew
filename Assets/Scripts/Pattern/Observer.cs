using System;
using UnityEngine;

namespace Pattern
{
    public class Observer : MonoBehaviour, IObserverCallbackable, IObserverListenable
    {
        private event Action Event;

        void IObserverCallbackable.OnCallback() => Event?.Invoke();

        void IObserverListenable.Subscribe(Action value)
        {
            Event += value;
        }

        void IObserverListenable.Unsubscribe(Action value)
        {
            Event -= value;
        }
    }
}