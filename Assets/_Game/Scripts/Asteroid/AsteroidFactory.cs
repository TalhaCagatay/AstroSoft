using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Game.Scripts.Game;
using UnityEngine;

namespace _Game.Scripts.Asteroid
{
    public enum AsteroidType
    {
        BigAsteroid,
        MediumAsteroid,
        SmallAsteroid
    }
    
    public abstract class Asteroid
    {
        public virtual AsteroidType AsteroidType { get; }
    }

    public class BigAsteroid : Asteroid
    {
        public override AsteroidType AsteroidType => AsteroidType.BigAsteroid;
    }

    public class MediumAsteroid : Asteroid
    {
        public override AsteroidType AsteroidType => AsteroidType.MediumAsteroid;
    }
    
    public class SmallAsteroid : Asteroid
    {
        public override AsteroidType AsteroidType => AsteroidType.SmallAsteroid;
    }

    public static class AsteroidFactory
    {
        private static Dictionary<AsteroidType, Type> _asteroidsByName;
        private static bool IsInitialized => _asteroidsByName != null;

        private static void InitializeFactory()
        {
            if (IsInitialized) return;

            var asteroidTypes = Assembly.GetAssembly(typeof(Asteroid)).GetTypes()
                .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(Asteroid)));

            _asteroidsByName = new Dictionary<AsteroidType, Type>();
            foreach (var asteroidType in asteroidTypes)
            {
                var tempEffect = Activator.CreateInstance(asteroidType) as Asteroid;
                _asteroidsByName.Add(tempEffect.AsteroidType, asteroidType);
            }
        }

        public static Asteroid GetAsteroid(AsteroidType asteroidType)
        {
            InitializeFactory();
            if (_asteroidsByName.ContainsKey(asteroidType))
            {
                var type = _asteroidsByName[asteroidType];
                var asteroid = Activator.CreateInstance(type) as Asteroid;
                return asteroid;
            }

            return null;
        }

        internal static IEnumerable<AsteroidType> GetAbilityNames()
        {
            Debug.Log($"{LOGS.HEAD_LOG} Test");
            InitializeFactory();
            return _asteroidsByName.Keys;
        }
    }
}