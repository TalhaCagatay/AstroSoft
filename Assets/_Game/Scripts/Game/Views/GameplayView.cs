using System;
using System.Collections.Generic;
using _Game.Scripts.Asteroid;
using _Game.Scripts.Game.Configs;
using _Game.Scripts.Game.Controllers;
using _Game.Scripts.Game.Levels;
using _Game.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Game.Views
{
    public class GameplayView : MonoBehaviour, IView
    {
        public event Action<IView> ViewOpened;
        public event Action<IView> ViewClosed;

        [SerializeField] private RectTransform _shipLivesContainer;
        [SerializeField] private Image _shipLiveImage;
        [SerializeField] private TextMeshProUGUI _currentScoreText;

        private List<Image> _shipLiveImages = new List<Image>();
        
        public ILevel Level { get; set; }

        public void Init()
        {
            GameController.Instance.StateChanged += OnStateChanged;
            GameController.Instance.PlayerController.PlayerLostLive += OnPlayerLostLive;
            ResetScore();
        }

        private void OnStateChanged(GameState newState)
        {
            if(newState == GameState.Play)
                InstantiateInitialShipLives();
        }

        private void InstantiateInitialShipLives()
        {
            for (var i = 0; i < GameConfig.Instance.ShipMaxLiveCount; i++)
                _shipLiveImages.Add(Instantiate(_shipLiveImage, _shipLivesContainer));
        }

        private void Update() => SetScore(PlayerScoreBehaviour.CurrentScore);

        private void ResetScore() => SetScore(0);

        private void SetScore(int currentScore) => _currentScoreText.text = $"Score : {currentScore}";

        private void OnPlayerLostLive(int remainingLive)
        {
            var lastImage = _shipLiveImages[_shipLiveImages.Count - 1];
            Destroy(lastImage);
            _shipLiveImages.Remove(lastImage);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            ViewOpened?.Invoke(this);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            ViewClosed?.Invoke(this);
        }
    }
}