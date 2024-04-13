using System;
using Characters;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Environment
{
    public class CollisionDetect : MonoBehaviour
    {
        [SerializeField] private CharacterType characterCollisionReact;
        public Button.ButtonClickedEvent EnterEvent;
        public Button.ButtonClickedEvent StayEvent;
        public Button.ButtonClickedEvent ExitEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IAttackable attackable)) return;
            if (attackable.CharacterType != characterCollisionReact) return;
            EnterEvent?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IAttackable attackable)) return;
            if (attackable.CharacterType != characterCollisionReact) return;
            ExitEvent?.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IAttackable attackable)) return;
            if (attackable.CharacterType != characterCollisionReact) return;
            StayEvent?.Invoke();
        }
    }
}