using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Play.Death
{
    public class Controller : UIBehaviour
    {
        [SerializeField] private Button restart;
        [SerializeField] private Button exit;
        public event Action RestartEvent;
        public event Action ExitEvent;
        private void Awake()
        {
            restart.onClick.AddListener(OnRestartEvent);
            exit.onClick.AddListener(OnExitEvent);
        }
        
        protected virtual void OnRestartEvent()
        {
            RestartEvent?.Invoke();
        }

        protected virtual void OnExitEvent()
        {
            ExitEvent?.Invoke();
        }
    }
}