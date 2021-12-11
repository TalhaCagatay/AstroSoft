using System;
using _Game.Scripts.Game.Controllers;
using _Game.Scripts.Game.Levels;
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
        
        public ILevel Level { get; set; }

        public void Init()
        {
            _startButton.onClick.AddListener(OnStartClicked);
            GameController.Instance.PrefsController.HighScoreChanged += OnHighScoreChanged;
            SetHighScoreText(GameController.Instance.PrefsController.GetHighScore());
        }

        private void OnHighScoreChanged(int newHighScore) => SetHighScoreText(newHighScore);

        private void SetHighScoreText(int newHighScore) => _highScoreText.text = $"High Score : {newHighScore}";

        protected void OnStartClicked() => StartClicked?.Invoke();

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