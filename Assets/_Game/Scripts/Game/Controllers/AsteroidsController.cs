using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Asteroid;
using _Game.Scripts.Configs.AsteroidConfig;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Game.Controllers
{
    public class AsteroidsController : MonoBehaviour, IAsteroidController
    {
        public event Action Initialized;
        public event Action Disposed;

        [SerializeField] private AsteroidConfig _asteroidConfig;

        private List<AsteroidBehaviour> _spawnedAsteroids = new List<AsteroidBehaviour>();
        private Coroutine _asteroidSpawnRoutine;
        private bool _isInitialized;
        private float _diameter;
        private DeviceOrientation _deviceOrientation;

        public bool IsInitialized => _isInitialized;
        public void Init()
        {
            GameController.Instance.PlayerController.PlayerLostLive += OnPlayerLostLive;
            CalculateDiameterForSpawningOutsideCamera();
            GameController.Instance.StateChanged += OnStateChanged;
            _deviceOrientation = Input.deviceOrientation;
            _isInitialized = true;
            Initialized?.Invoke();
        }

        private void Update()
        {
            if (IsOrientationChanged())
                ChangeOrientationAndReset();
        }

        private bool IsOrientationChanged() => _deviceOrientation != Input.deviceOrientation;

        private void ChangeOrientationAndReset()
        {
            _deviceOrientation = Input.deviceOrientation;
            CalculateDiameterForSpawningOutsideCamera();
        }

        private void OnPlayerLostLive(int remainingLive)
        {
            ClearAsteroids();
            if (remainingLive > 0)
            {
                StopSpawningCoroutine();
                Invoke("StartSpawningCoroutine", GameController.Instance.GameConfig.PlayerRespawnTime);
            }
            else
                StopSpawningCoroutine();
        }

        private void OnStateChanged(GameState newState)
        {
            if (newState == GameState.Play)
                StartSpawningCoroutine();
            else
                StopSpawningCoroutine();
        }

        private void ClearAsteroids()
        {
            for (var i = 0; i < _spawnedAsteroids.Count; i++)
            {
                _spawnedAsteroids[i].AsteroidDivided -= OnAsteroidDivided;
                LeanPool.Despawn(_spawnedAsteroids[i].gameObject);
            }
            
            _spawnedAsteroids.Clear();
        }
        
        private void StartSpawningCoroutine() => _asteroidSpawnRoutine = StartCoroutine(AsteroidSpawningCoroutine());

        private void StopSpawningCoroutine()
        {
            if (_asteroidSpawnRoutine == null) return;
            StopCoroutine(_asteroidSpawnRoutine);
        }

        private void CalculateDiameterForSpawningOutsideCamera()
        {
            var cameraHeight = GameController.Instance.GameConfigMono.MainCamera.orthographicSize;
            var cameraWidth = GameController.Instance.GameConfigMono.MainCamera.orthographicSize * GameController.Instance.GameConfigMono.MainCamera.aspect;
            var sumOfSquares = Mathf.Pow(cameraHeight, 2) + Mathf.Pow(cameraWidth, 2);
            _diameter = Mathf.Sqrt(sumOfSquares);
        }

        private IEnumerator AsteroidSpawningCoroutine()
        {
            while (true)
            {
                if (_spawnedAsteroids.Count < GameController.Instance.GameConfig.MaxAmountOfAsteroids)
                {
                    var spawnPosition = CalculateRandomSpawnPosition();
                    SpawnAsteroid(spawnPosition).AsteroidDivided += OnAsteroidDivided;    
                }
                
                yield return new WaitForSeconds(GameController.Instance.GameConfig.AsteroidSpawnFrequencyInSeconds);
            }
        }

        private void OnAsteroidDivided(IAsteroid asteroid, Vector2 damagePosition)
        {
            asteroid.AsteroidDivided -= OnAsteroidDivided;
            _spawnedAsteroids.Remove((AsteroidBehaviour) asteroid);
            if (asteroid.Health < 1) return;
            for (var i = 0; i < GameController.Instance.GameConfig.AsteroidDivisionCountUponDestroyed; i++)
            {
                var movementDirection = Random.insideUnitCircle.normalized;
                SpawnAsteroid(asteroid.AsteroidConfig.DividedAsteroidConfig, asteroid.AsteroidTransform.position, movementDirection, asteroid.Health).AsteroidDivided += OnAsteroidDivided;
            }
        }

        private Vector2 CalculateRandomSpawnPosition() => Random.insideUnitCircle.normalized * _diameter;

        private AsteroidBehaviour SpawnAsteroid(Vector2 positionToSpawn)
        {
            var asteroidMovementDirection = Vector2.zero - positionToSpawn;
            var asteroidBehaviour = LeanPool.Spawn(_asteroidConfig.AsteroidBehaviour, positionToSpawn, Quaternion.identity, GameController.Instance.GameConfigMono.AsteroidsParent);
            asteroidBehaviour.Init(_asteroidConfig, asteroidMovementDirection, _asteroidConfig.AsteroidHealth);
            _spawnedAsteroids.Add(asteroidBehaviour);
            return asteroidBehaviour;
        }
        
        private AsteroidBehaviour SpawnAsteroid(AsteroidConfig asteroidConfig, Vector2 positionToSpawn, Vector2 movementDirection, int health)
        {
            var asteroidBehaviour = LeanPool.Spawn(asteroidConfig.AsteroidBehaviour, positionToSpawn, Quaternion.identity, GameController.Instance.GameConfigMono.AsteroidsParent);
            asteroidBehaviour.Init(asteroidConfig, movementDirection, health);
            _spawnedAsteroids.Add(asteroidBehaviour);
            return asteroidBehaviour;
        }

        public void Dispose()
        {
            StopSpawningCoroutine();
            _isInitialized = false;
            Disposed?.Invoke();
        }
    }
}