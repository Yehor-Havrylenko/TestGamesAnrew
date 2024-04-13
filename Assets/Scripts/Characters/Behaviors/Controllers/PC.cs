using System;
using Pattern;
using UnityEngine;

namespace Characters.Behaviors.Controllers
{
    public class PC : MonoBehaviour
    {
        [SerializeField] private Observer death;
        [SerializeField] private Observer restart;
        [SerializeField] private Observer stop;
        [SerializeField] private Observer continueGame;
        private IObserverListenable _deathListenable;
        private IObserverListenable _restartListenable;
        private IObserverListenable _continueListenable;
        private IObserverListenable _stopListenable;
        private IObserverCallbackable _stopCallbackable;
        private bool _isStop;
        public event Action<Vector3> DirectionMoveEvent;
        public event Action<Vector3> DirectionRotateEvent;
        public event Action ShootEvent;
        public event Action UltaEvent;

        private void Awake()
        {
            _deathListenable = death;
            _restartListenable = restart;
            _continueListenable = continueGame;
            _stopCallbackable = stop;
            _stopListenable = stop;
            _stopListenable.Subscribe(OnUnlockCursor);
            _continueListenable.Subscribe(OnlockCursor);
            _restartListenable.Subscribe(OnlockCursor);
            _deathListenable.Subscribe(OnUnlockCursor);
        }

        private void OnUnlockCursor()
        {
            _isStop = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnlockCursor()
        {
            _isStop = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void Update()
        {
            if(_isStop) return;
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if(Input.GetKeyDown(KeyCode.R)) ShootEvent?.Invoke();
            DirectionMoveEvent?.Invoke(move);
            if(Input.GetKeyDown(KeyCode.U)) UltaEvent?.Invoke();
            if (Input.GetKeyDown(KeyCode.Escape)) _stopCallbackable.OnCallback();
        }

        private void LateUpdate()
        {
            if(_isStop) return;
            Vector3 rotate = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            DirectionRotateEvent?.Invoke(rotate);
        }
    }
}