using System;
using _Game.Scripts.Game.Controllers;
using _Game.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Game.Views
{
    public class LevelFailView : MonoBehaviour, IView
    {
        public event Action<IView> ViewOpened;
        public event Action<IView> ViewClosed;
        public event Action RestartClicked;
        public event Action ExitClicked;

        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        
        public void Init() => SetButtonListeners();

        private void SetButtonListeners()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnRestartClicked() => RestartClicked?.Invoke();
        
        private void OnExitClicked() => ExitClicked?.Invoke();

        public void Open()
        {
            SetScoreTexts();
            gameObject.SetActive(true);
            ViewOpened?.Invoke(this);
        }

        private void SetScoreTexts()
        {
            _currentScoreText.text = $"Current Score : {PlayerScoreBehaviour.CurrentScore}";
            _highScoreText.text = $"Current Score : {GameController.Instance.PrefsController.GetHighScore()}";
        }

        public void Close()
        {
            gameObject.SetActive(false);
            ViewClosed?.Invoke(this);
        }
    }
}