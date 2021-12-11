using System;
using _Game.Scripts.Game.Configs;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Game.Controllers
{
    public enum GameState
    {
        None,
        Menu,
        Play,
        Completed,
        Failed
    }
    public class GameController : MonoBehaviour, IGameController
    {
        #region Singleton

        public static GameController Instance = null;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            Init();
        }

        #endregion

        public event Action Initialized;
        public event Action Disposed;
        public event Action<GameState> StateChanged; 

        [SerializeField] private ViewController _viewController;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private AsteroidsController _asteroidsController;

        private bool _isInitialized;
        private GameState _gameState;

        public GameState GameState => _gameState;
        public IPlayerController PlayerController { get; set; }
        public IPrefsController PrefsController { get; set; }
        public IViewController ViewController => _viewController;
        public IAsteroidController AsteroidController => _asteroidsController;

        public bool IsInitialized => _isInitialized;
        public void Init()
        {
            Application.targetFrameRate = 60;
            Debug.unityLogger.logEnabled = true;
            
            PrefsController = new PrefsController();
            PlayerController = LeanPool.Spawn(_playerController, GameConfig.Instance.LevelParent);
            
            PrefsController.Init();
            ViewController.Init();
            PlayerController.Init();
            AsteroidController.Init();

            ViewController.StartClicked += OnStartClicked;
            ViewController.RetryClicked += OnRetryClicked;
            ViewController.ExitClicked += OnExitClicked;
            PlayerController.PlayerDied += OnPlayerDied;
            
            _isInitialized = true;
            Initialized?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Initialized");
            
            ChangeState(GameState.Menu);
        }

        private void OnExitClicked() => ChangeState(GameState.Menu);

        private void OnRetryClicked() => ChangeState(GameState.Play);

        private void OnPlayerDied() => ChangeState(GameState.Failed);

        public void OnContinueClicked() => ReturnToMenu();

        public void OnMultiplierClicked() => ReturnToMenu();

        private void ReturnToMenu()
        {
            Debug.Log($"{LOGS.HEAD_LOG} Returning to menu");
            ChangeState(GameState.Menu);
        }

        private void OnStartClicked() => ChangeState(GameState.Play);

        private void ChangeState(GameState newState)
        {
            _gameState = newState;
            StateChanged?.Invoke(_gameState);
        }

        public void Dispose()
        {
            PrefsController.Dispose();
            ViewController.Dispose();

            ViewController.StartClicked -= OnStartClicked;
            ViewController.RetryClicked -= OnRetryClicked;
            PlayerController.PlayerDied -= OnPlayerDied;
            
            _isInitialized = false;
            Disposed?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Disposed");
        }
    }
}