using System;
using System.Collections;
using UI.Joystick;
using UI.Touches;
using UnityEngine;

namespace Characters.Behaviors.Controllers
{
    public class Mobile : MonoBehaviour
    {
        [SerializeField] private JoystickHandler movement;
        [SerializeField] private JoystickHandler attack;
        [SerializeField] private Rotate rotate;
        public event Action<Vector3> MovementEvent;
        public event Action<Vector3> RotateAttackEvent;
        public event Action<Vector3> RotateEvent;
        public event Action ShootEvent;
        private Coroutine _shoot;

        private void Awake()
        {
            movement.DirectionEvent += OnMovementEvent;
            attack.PointerDownEvent += OnStartShoot;
            attack.PointerUpEvent += OnStopShoot;
            rotate.DirectionEvent += OnRotateEvent;
        }

        protected virtual void OnMovementEvent(Vector3 obj)
        {
            MovementEvent?.Invoke(obj);
        }

        protected virtual void OnRotateAttackEvent(Vector3 obj)
        {
            RotateAttackEvent?.Invoke(obj);
        }

        protected virtual void OnStartShoot()
        {
            _shoot = StartCoroutine(Shoot());
        }

        protected virtual void OnStopShoot()
        {
            if(_shoot!=null) StopCoroutine(_shoot);
        }

        private IEnumerator Shoot()
        {
            for (;;)
            {
                yield return new WaitForSeconds(0.1f);
                ShootEvent?.Invoke();
            }
        }

        protected virtual void OnRotateEvent(Vector3 obj)
        {
            RotateEvent?.Invoke(obj);
        }
    }
}