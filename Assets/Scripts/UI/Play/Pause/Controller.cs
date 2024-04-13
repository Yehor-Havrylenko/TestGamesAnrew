using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Play.Pause
{
    public class Controller : UIBehaviour
    {
        public event Action ContinueEvent;
        [SerializeField] private Button continueBtn;

        private void Start()
        {
            continueBtn.onClick.AddListener(OnContinueEvent);
        }

        private void OnContinueEvent()
        {
            ContinueEvent?.Invoke();
        }
    }
}

