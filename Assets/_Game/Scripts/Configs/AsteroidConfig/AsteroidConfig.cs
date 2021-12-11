using _Game.Scripts.Asteroid;
using UnityEngine;

namespace _Game.Scripts.Configs.AsteroidConfig
{
    [CreateAssetMenu(fileName = "DefaultAsteroid", menuName = "AstroSoft/Asteroids/DefaultAsteroid", order = 2)]
    public class AsteroidConfig : ScriptableObject
    {
        public AsteroidBehaviour AsteroidBehaviour;
        public AsteroidConfig DividedAsteroidConfig;
        public float AsteroidMovementSpeed = 1f;
        public int AsteroidHealth = 3;
        public int AsteroidScore = 10;
    }
}