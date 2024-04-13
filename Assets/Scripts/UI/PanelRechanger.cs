namespace UI
{
    public class PanelRechanger
    {
        private IHidenable _currentPanel;

        public void SetNewPanel(IHidenable newPanel)
        {
            if (_currentPanel == newPanel) return;
            if (_currentPanel != null)
            {
                _currentPanel.Hide();
            }

            _currentPanel = newPanel;
            _currentPanel.Show();
        }
    }
}