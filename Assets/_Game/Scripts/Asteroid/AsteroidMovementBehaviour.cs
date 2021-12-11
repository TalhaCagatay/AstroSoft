using _Game.Scripts.Configs.AsteroidConfig;
using UnityEngine;

namespace _Game.Scripts.Asteroid
{
    public class AsteroidMovementBehaviour : MonoBehaviour
    {
        private AsteroidConfig _asteroidConfig;
        private Vector3 _movementDirection;

        public void Init(AsteroidConfig asteroidConfig, Vector3 movementDirection)
        {
            _asteroidConfig = asteroidConfig;
            _movementDirection = movementDirection;
        }

        private void Update() =>
            transform.Translate(_movementDirection.normalized * (_asteroidConfig.AsteroidMovementSpeed * Time.deltaTime), Space.World);
    }
}