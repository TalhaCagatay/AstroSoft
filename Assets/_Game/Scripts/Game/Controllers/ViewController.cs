using System;
using System.Collections.Generic;
using _Game.Scripts.Game.Levels;
using _Game.Scripts.Game.Views;
using UnityEngine;

namespace _Game.Scripts.Game.Controllers
{
    public class ViewController : MonoBehaviour, IViewController
    {
        public event Action Initialized;
        public event Action Disposed;
        public event Action StartClicked;
        public event Action RetryClicked;
        public event Action ExitClicked;

        private bool _isInitialized;
        public bool IsInitialized => _isInitialized;
        
        [SerializeField] private GameObject _gameplayScreenGameObject;
        [SerializeField] private MenuView _menuView;
        [SerializeField] private LevelCompleteView _levelCompleteView;
        [SerializeField] private LevelFailView _levelFailView;
        
        public Queue<IView> ActiveViews { get; set; }
        public Queue<IView> ActivePopups { get; set; }
        public IView MenuView { get; set; }
        public IView LevelCompleteView { get; set; }
        public IView LevelFailView { get; set; }
        public IView GameplayView { get; set; }
        public IView SplashView { get; set; }

        public void Init()
        {
            LevelFailView = _levelFailView.GetComponent<IView>();
            LevelCompleteView = _levelCompleteView.GetComponent<IView>();
            GameplayView = _gameplayScreenGameObject.GetComponent<IView>();
            MenuView = _menuView.GetComponent<IView>();
            ((MenuView) MenuView).StartClicked += OnStartClicked;
            ((LevelFailView) LevelFailView).RestartClicked += OnRetryClicked;
            ((LevelFailView) LevelFailView).ExitClicked += OnExitClicked;
            GameController.Instance.StateChanged += OnStateChanged;
            InitializeViews();
            _isInitialized = true;
            Initialized?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Initialized");
        }

        private void OnExitClicked() => ExitClicked?.Invoke();

        private void OnRetryClicked() => RestartButton();

        private void OnStartClicked() => StartButton();

        protected virtual void OnStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.None:
                    break;
                case GameState.Menu:
                    ChangeView(MenuView);
                    break;
                case GameState.Play:
                    ChangeView(GameplayView);
                    break;
                case GameState.Completed:
                    ChangeView(LevelCompleteView);
                    break;
                case GameState.Failed:
                    ChangeView(LevelFailView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void InitializeViews()
        {
            LevelCompleteView.Init();
            LevelFailView.Init();
            GameplayView.Init();
            MenuView.Init();
            ActiveViews = new Queue<IView>();
            ActivePopups = new Queue<IView>();
            ChangeView(MenuView);
        }

        private void OnDestroy() => Dispose();

        public void Dispose()
        {
            _isInitialized = false;
            Disposed?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Disposed");
        }
        
        public void ChangeView(IView viewToOpen)
        {
            if (ActiveViews.Count > 0)
            {
                ActiveViews.Dequeue().Close();
            }
            ActiveViews.Enqueue(viewToOpen);
            ActiveViews.Peek().Open();
        }

        public void OpenPopup(IView popupToOpen)
        {
            ActivePopups.Enqueue(popupToOpen);
            ActivePopups.Peek().Open();
        }

        public void ClosePopup()
        {
            if (ActivePopups.Count > 0)
            {
                ActivePopups.Dequeue().Close();
            }
        }
        
        public void StartButton()
        {
            StartClicked?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} Start button clicked");
        }
        
        public void RestartButton()
        {
            RetryClicked?.Invoke();
            ChangeView(GameplayView);
            Debug.Log($"{LOGS.HEAD_LOG} Restart button clicked");
        }
    }
}