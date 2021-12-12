using _Game.Scripts.Asteroid;
using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Game.Particle
{
    public class ParticleCallbackBridge
    {
        private Transform _shipTransform;
        public void Init()
        {
            GetShipTransform();
            SubscribeEventsForParticle();
        }

        private void SubscribeEventsForParticle()
        {
            GameController.Instance.PlayerController.PlayerLostLive += OnPlayerLostLive;
            AsteroidBehaviour.AsteroidDestroyed += OnAsteroidDestroyed;
        }

        private void GetShipTransform() => _shipTransform = ((PlayerController) GameController.Instance.PlayerController).TransformToMove;

        private void OnAsteroidDestroyed(int arg1, Vector2 damagePoint) => GameController.Instance.ParticleController.SpawnParticleAtPosition(ParticleController.ParticleType.Shoot, damagePoint);
        
        private void OnPlayerLostLive(int obj) => GameController.Instance.ParticleController.SpawnParticleAtPosition(ParticleController.ParticleType.Explosion, _shipTransform.position);
    }
}