using System;
using _Game.Scripts.Asteroid;
using _Game.Scripts.Game.Configs;
using _Game.Scripts.Player;
using _Game.Scripts.Player.Movement;
using _Game.Scripts.Player.Shooting;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Game.Controllers
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        public event Action<int> PlayerLostLive;
        public event Action PlayerDied;
        
        public event Action Initialized;
        public event Action Disposed;

        [SerializeField] private Transform _transformToMove;
        [SerializeField] private SpriteRenderer _shipSpriteRenderer;
        [SerializeField] private PlayerMovementBehaviour _playerMovementBehaviour;
        [SerializeField] private PlayerShootBehaviour _playerShootBehaviour;
        [SerializeField] private PlayerDamageableBehaviour _playerDamageableBehaviour;
        [SerializeField] private PlayerScoreBehaviour _playerScoreBehaviour;
        
        private Vector2 _initialShipPosition;
        private bool _isInitialized;
        public Transform TransformToMove => _transformToMove;
        public SpriteRenderer ShipSpriteRenderer => _shipSpriteRenderer;
        public bool IsInitialized => _isInitialized;
        
        public void Init()
        {
            GetInitialShipPosition();
            DisableShip();
            InitializePlayerDependencies();
            HandleSubscriptions();

            _isInitialized = true;
            Initialized?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Initialized");
        }

        private void HandleSubscriptions()
        {
            _playerDamageableBehaviour.PlayerGotHit += OnPlayerGotHit;
            GameController.Instance.StateChanged += OnStateChanged;
        }

        private void GetInitialShipPosition() => _initialShipPosition = _transformToMove.position;

        private void InitializePlayerDependencies()
        {
            _playerScoreBehaviour.Init();
            _playerMovementBehaviour.Init();
            _playerDamageableBehaviour.Init(GameConfig.Instance.ShipMaxLiveCount);
        }

        private void ResetShip()
        {
            ResetShipPosition();
            ResetShipRotation();
        }

        private void ResetShipRotation() => _transformToMove.rotation = Quaternion.identity;

        private void ResetShipPosition() => _transformToMove.position = _initialShipPosition;

        private void EnableShip()
        {
            ResetShip();
            _transformToMove.gameObject.SetActive(true);
            _playerMovementBehaviour.enabled = true;
            _playerShootBehaviour.enabled = true;
        }

        private void DisableShip()
        {
            _transformToMove.gameObject.SetActive(false);
            _playerMovementBehaviour.enabled = false;
            _playerShootBehaviour.enabled = false;
        }

        private void OnPlayerGotHit(int remainingLives)
        {
            PlayerLostLive?.Invoke(remainingLives);
            DisableShip();

            if(remainingLives <= 0)
                PlayerDied?.Invoke();
            else
                Invoke("RespawnPlayer", GameConfig.Instance.PlayerRespawnTime);
        }

        private void RespawnPlayer() => EnableShip();
        
        private void OnStateChanged(GameState newState)
        {
            if (newState == GameState.Play)
                EnableShip();
        }

        public void Dispose()
        {
            HandleUnSubscriptions();
            LeanPool.Despawn(gameObject);
            _isInitialized = false;
            Disposed?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Disposed");
        }

        private void HandleUnSubscriptions()
        {
            _playerDamageableBehaviour.PlayerGotHit -= OnPlayerGotHit;
            GameController.Instance.StateChanged -= OnStateChanged;
        }
    }
}