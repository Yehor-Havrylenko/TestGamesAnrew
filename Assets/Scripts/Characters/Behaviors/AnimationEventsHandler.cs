using System;
using UnityEngine;

namespace Characters.Behaviors
{
    public class AnimationEventsHandler : MonoBehaviour
    {
        public event Action DeathEvent;
        public void OnDeath()
        {
            DeathEvent?.Invoke();
        }
    }
}