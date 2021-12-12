using System;
using _Game.Scripts.Configs.GameConfig;
using _Game.Scripts.Initializations;
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
        [SerializeField] private SoundController _soundController;
        [SerializeField] private ParticleController _particleController;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private GameConfigMono _gameConfigMono;
        [SerializeField] private InitStep _mainInitStep;
        
        private bool _isInitialized;
        private GameState _gameState;

        public GameState GameState => _gameState;
        public IPlayerController PlayerController { get; set; }
        public IPrefsController PrefsController { get; set; }
        public IViewController ViewController => _viewController;
        public IAsteroidController AsteroidController => _asteroidsController;
        public ISoundController SoundController => _soundController;
        public IParticleController ParticleController => _particleController;
        public GameConfig GameConfig => _gameConfig;
        public GameConfigMono GameConfigMono => _gameConfigMono;

        public bool IsInitialized => _isInitialized;
        public void Init()
        {
            CreatePlayer();
            SubscribeGameControllerDependencies();
            InitializeInitSteps();
        }

        private void InitializeInitSteps()
        {
            _mainInitStep.Initialized += OnInitialized;
            _mainInitStep.Run();
        }

        private void CreatePlayer()
        {
            PlayerController = LeanPool.Spawn(_playerController, GameConfigMono.LevelParent);
        }

        private void OnInitialized()
        {
            _mainInitStep.Initialized -= OnInitialized;
            _isInitialized = true;
            Initialized?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Initialized");
            ChangeState(GameState.Menu);
        }

        private void SubscribeGameControllerDependencies()
        {
            ViewController.StartClicked += OnStartClicked;
            ViewController.RetryClicked += OnRetryClicked;
            ViewController.ExitClicked += OnExitClicked;
            PlayerController.PlayerDied += OnPlayerDied;
        }
        
        private void UnSubscribeGameControllerDependencies()
        {
            ViewController.StartClicked -= OnStartClicked;
            ViewController.RetryClicked -= OnRetryClicked;
            ViewController.ExitClicked -= OnExitClicked;
            PlayerController.PlayerDied -= OnPlayerDied;
        }

        private void OnExitClicked() => ChangeState(GameState.Menu);

        private void OnRetryClicked() => ChangeState(GameState.Play);

        private void OnPlayerDied() => ChangeState(GameState.Failed);

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
            PlayerController.Dispose();
            AsteroidController.Dispose();

            UnSubscribeGameControllerDependencies();
            
            _isInitialized = false;
            Disposed?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Disposed");
        }
    }
}