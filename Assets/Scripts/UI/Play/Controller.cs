using Characters.Player;
using Pattern;
using UnityEngine;

namespace UI.Play
{
    public class Controller : MonoBehaviour
    {
        [Header("Screens")] 
        [SerializeField] private Game.Controller gameScreen;
        [SerializeField] private Death.Controller deathScreen;
        [SerializeField] private Pause.Controller pauseScreen;
        [Header("Observers")] 
        [SerializeField] private Observer restart;
        [SerializeField] private Observer death;
        [SerializeField] private Observer stop;
        [SerializeField] private Observer continueGame;
        [Header("Rest")] 
        [SerializeField] private Container player;
        private PanelRechanger _panelRechanger;
        private IObserverCallbackable _restartCallbackable;
        private IObserverListenable _stopListenable;
        private IObserverCallbackable _stopCallbackable;
        private IObserverCallbackable _deathCallbackable;
        private IObserverCallbackable _continueCallbackable;

        private void Awake()
        {
            _panelRechanger = new PanelRechanger();
            gameScreen.Init(player, Application.isMobilePlatform);
            player.Heal.DeathEvent += Death;
            _restartCallbackable = restart;
            _deathCallbackable = death;
            _stopCallbackable = stop;
            _continueCallbackable = continueGame;
            _stopListenable = stop;
            _stopListenable.Subscribe(Pause);
            ScreensSubscribes();
            Game();
        }

        private void ScreensSubscribes()
        {
            gameScreen.PauseEvent += Stop;
            deathScreen.ExitEvent += Exit;
            deathScreen.RestartEvent += Restart;
            pauseScreen.ContinueEvent += Continue;
        }

        #region Screens

        private void Game()
        {
            _panelRechanger.SetNewPanel(gameScreen);
        }

        private void Pause()
        {
            _panelRechanger.SetNewPanel(pauseScreen);
        }

        private void Death()
        {
            _deathCallbackable.OnCallback();
            _panelRechanger.SetNewPanel(deathScreen);
        }

        #endregion

        #region ScreenHandlers

        private void Continue()
        {
            _continueCallbackable.OnCallback();
            Game();
        }

        private void Restart()
        {
            _restartCallbackable.OnCallback();
            Game();
        }

        private void Stop()
        {
            _stopCallbackable.OnCallback();
        }

        private void Exit()
        {
            Application.Quit();
        }

        #endregion
    }
}