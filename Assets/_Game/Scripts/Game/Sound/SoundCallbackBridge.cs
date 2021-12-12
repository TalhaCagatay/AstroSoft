using _Game.Scripts.Asteroid;
using _Game.Scripts.Game.Controllers;
using _Game.Scripts.Player.Shooting;
using UnityEngine;

namespace _Game.Scripts.Game.Sound
{
    public class SoundCallbackBridge
    {
        public void Init() => SubscribeEventsForSound();

        private void SubscribeEventsForSound()
        {
            PlayerShootBehaviour.PlayerShoot += OnPlayerShoot;
            GameController.Instance.PlayerController.PlayerLostLive += OnPlayerLostLive;
            PlayerController.ShipRespawned += OnShipRespawned;
            AsteroidBehaviour.AsteroidDestroyed += OnAsteroidDestroyed;
        }

        private void OnAsteroidDestroyed(int obj, Vector2 damagePosition) => GameController.Instance.SoundController.PlaySound(SoundController.SoundType.Shot);

        private void OnShipRespawned() => GameController.Instance.SoundController.PlaySound(SoundController.SoundType.Respawn);

        private void OnPlayerShoot() => GameController.Instance.SoundController.PlaySound(SoundController.SoundType.Shot);
        
        private void OnPlayerLostLive(int remainingLives) => GameController.Instance.SoundController.PlaySound(remainingLives > 0 ? SoundController.SoundType.Explosion : SoundController.SoundType.Death);
    }
}