using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Asteroid;
using _Game.Scripts.Configs.AsteroidConfig;
using _Game.Scripts.Game.Configs;
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

        public bool IsInitialized => _isInitialized;
        public void Init()
        {
            GameController.Instance.PlayerController.PlayerLostLive += OnPlayerLostLive;
            CalculateDiameterForSpawningOutsideCamera();
            GameController.Instance.StateChanged += OnStateChanged;
            _isInitialized = true;
            Initialized?.Invoke();
        }

        private void OnPlayerLostLive(int remainingLive)
        {
            ClearAsteroids();
            if (remainingLive > 0)
            {
                StopSpawningCoroutine();
                Invoke("StartSpawningCoroutine", GameConfig.Instance.PlayerRespawnTime);
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
                LeanPool.Despawn(_spawnedAsteroids[i]);
            }
            
            _spawnedAsteroids.Clear();
        }
        
        private void StartSpawningCoroutine() => _asteroidSpawnRoutine = StartCoroutine(StartAsteroidSpawning());

        private void StopSpawningCoroutine()
        {
            if (_asteroidSpawnRoutine == null) return;
            StopCoroutine(_asteroidSpawnRoutine);
        }

        private void CalculateDiameterForSpawningOutsideCamera()
        {
            var cameraHeight = GameConfig.Instance.MainCamera.orthographicSize;
            var cameraWidth = GameConfig.Instance.MainCamera.orthographicSize * GameConfig.Instance.MainCamera.aspect;
            var sumOfSquares = Mathf.Pow(cameraHeight, 2) + Mathf.Pow(cameraWidth, 2);
            _diameter = Mathf.Sqrt(sumOfSquares);
        }

        private IEnumerator StartAsteroidSpawning()
        {
            while (true)
            {
                var spawnPosition = CalculateRandomSpawnPosition();
                SpawnAsteroid(spawnPosition).AsteroidDivided += OnAsteroidDivided;
                yield return new WaitForSeconds(3f);
            }
        }

        private void OnAsteroidDivided(IAsteroid asteroid, Vector2 damagePosition)
        {
            asteroid.AsteroidDivided -= OnAsteroidDivided;
            if (asteroid.Health < 1) return;
            for (var i = 0; i < 2; i++)
            {
                var movementDirection = Random.insideUnitCircle.normalized;
                SpawnAsteroid(asteroid.AsteroidConfig.DividedAsteroidConfig, asteroid.AsteroidTransform.position, movementDirection, asteroid.Health).AsteroidDivided += OnAsteroidDivided;
            }
        }

        private Vector2 CalculateRandomSpawnPosition() => Random.insideUnitCircle.normalized * _diameter;

        private AsteroidBehaviour SpawnAsteroid(Vector2 positionToSpawn)
        {
            var asteroidMovementDirection = Vector2.zero - positionToSpawn;
            var asteroidBehaviour = LeanPool.Spawn(_asteroidConfig.AsteroidBehaviour, positionToSpawn, Quaternion.identity, GameConfig.Instance.AsteroidsParent);
            asteroidBehaviour.Init(_asteroidConfig, asteroidMovementDirection, _asteroidConfig.AsteroidHealth);
            _spawnedAsteroids.Add(asteroidBehaviour);
            return asteroidBehaviour;
        }
        
        private AsteroidBehaviour SpawnAsteroid(AsteroidConfig asteroidConfig, Vector2 positionToSpawn, Vector2 movementDirection, int health)
        {
            var asteroidBehaviour = LeanPool.Spawn(asteroidConfig.AsteroidBehaviour, positionToSpawn, Quaternion.identity, GameConfig.Instance.AsteroidsParent);
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