using System;
using _Game.Scripts.Game.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Game.Views
{
    public class MenuView : MonoBehaviour, IView
    {
        public event Action<IView> ViewOpened;
        public event Action<IView> ViewClosed;
        public event Action StartClicked;

        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        
        public void Init()
        {
            SetButtonListeners();
            SubscribeToEvents();
            SetHighScoreText(GameController.Instance.PrefsController.GetHighScore());
        }

        private void SubscribeToEvents() => GameController.Instance.PrefsController.HighScoreChanged += OnHighScoreChanged;

        private void SetButtonListeners() => _startButton.onClick.AddListener(OnStartClicked);

        private void OnHighScoreChanged(int newHighScore) => SetHighScoreText(newHighScore);

        private void SetHighScoreText(int newHighScore) => _highScoreText.text = $"High Score : {newHighScore}";

        private void OnStartClicked() => StartClicked?.Invoke();

        public void Open()
        {
            gameObject.SetActive(true);
            ViewOpened?.Invoke(this);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            GameController.Instance.PrefsController.HighScoreChanged -= OnHighScoreChanged;
            ViewClosed?.Invoke(this);
        }
    }
}