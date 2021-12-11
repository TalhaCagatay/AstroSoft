using System;
using _Game.Scripts.Configs.AsteroidConfig;
using UnityEngine;

namespace _Game.Scripts.Asteroid
{
    public interface IAsteroid : IDamageable
    {
        event Action<IAsteroid, Vector2> AsteroidDivided;
        Transform AsteroidTransform { get; }
        AsteroidConfig AsteroidConfig { get; }
        void Init(AsteroidConfig asteroidConfig, Vector3 movementDirection, int asteroidHealth);
    }
}