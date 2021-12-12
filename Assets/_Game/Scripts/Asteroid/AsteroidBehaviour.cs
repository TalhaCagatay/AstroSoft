using System;
using _Game.Scripts.Configs.AsteroidConfig;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Asteroid
{
    public class AsteroidBehaviour : MonoBehaviour, IAsteroid
    {
        public static event Action<int, Vector2> AsteroidDestroyed;
        public event Action<IAsteroid, Vector2> AsteroidDivided;
        
        [SerializeField] private AsteroidMovementBehaviour _asteroidMovementBehaviour;

        private AsteroidConfig _asteroidConfig;
        private int _health;
        private int _asteroidScore;
        public int Health => _health;

        public Transform AsteroidTransform => transform;
        public AsteroidConfig AsteroidConfig => _asteroidConfig;

        public void Init(AsteroidConfig asteroidConfig, Vector3 movementDirection, int asteroidHealth)
        {
            _asteroidConfig = asteroidConfig;
            _asteroidScore = asteroidConfig.AsteroidScore;
            _health = asteroidHealth;
            _asteroidMovementBehaviour.Init(asteroidConfig, movementDirection);
        }

        public void ApplyDamage(Vector2 damagePoint)
        {
            _health--;
            AsteroidDivided?.Invoke(this, damagePoint);
            AsteroidDestroyed?.Invoke(_asteroidScore, damagePoint);
            LeanPool.Despawn(gameObject);
        }
    }
}