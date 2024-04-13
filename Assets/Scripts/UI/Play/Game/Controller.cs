using Characters.Player;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Play.Game
{
    public class Controller : UIBehaviour
    {
        [SerializeField] private Button pause;
        [SerializeField] private ResourceSlider healthSlider;
        [SerializeField] private ResourceSlider manaSlider;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private CanvasGroup mobilePanel;

        public event Action PauseEvent;

        public void Init(Container player, bool isMobile)
        {
            pause.onClick.AddListener(OnPauseEvent);
            player.Score.UpdateEvent += (value, max) => scoreText.SetText($"Score {value.ToString()}");
            player.Heal.UpdateEvent += healthSlider.UpdateSlider;
            player.Mana.UpdateEvent += manaSlider.UpdateSlider;
            if (isMobile == false)
            {
                mobilePanel.alpha = 0;
                mobilePanel.interactable = false;
                mobilePanel.blocksRaycasts = false;
            }

            if (isMobile)
            {
                mobilePanel.alpha = 1;
                mobilePanel.interactable = true;
                mobilePanel.blocksRaycasts = true;
            }
        }

        private void OnPauseEvent()
        {
            PauseEvent?.Invoke();
        }
    }
}