using UnityEngine;

namespace _Game.Scripts.Configs.GameConfig
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "AstroSoft/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [Tooltip("General Despawning Time For Some Objects. Bullets for example")]
        public float DespawnAfterSeconds;
        public float PlayerRespawnTime;
        public float AsteroidSpawnFrequencyInSeconds = 3f;
        [Tooltip("Number of Sub Asteroids Count When Asteroid Destroyed")]
        public int AsteroidDivisionCountUponDestroyed = 2;
        public int ShipMaxLiveCount;
        [Range(0f, 1f)] public float SoundVolume = 0.5f;
    }
}