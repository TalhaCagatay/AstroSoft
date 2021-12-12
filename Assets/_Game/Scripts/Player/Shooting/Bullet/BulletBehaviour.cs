using _Game.Scripts.Configs.BulletConfig;
using _Game.Scripts.Game.Controllers;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Player.Shooting.Bullet
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private BulletMovementBehaviour _bulletMovementBehaviour;

        public void Init(BulletConfig bulletConfig)
        {
            InitializeBulletMovement(bulletConfig);
            SubscribeToEvents();
        }

        private void SubscribeToEvents() => GameController.Instance.StateChanged += OnStateChanged;
        private void UnSubscribeFromEvents() => GameController.Instance.StateChanged -= OnStateChanged;

        private void InitializeBulletMovement(BulletConfig bulletConfig) => _bulletMovementBehaviour.Init(bulletConfig);

        private void OnDisable() => UnSubscribeFromEvents();

        private void OnStateChanged(GameState newState)
        {
            if(newState == GameState.Failed)
                DespawnBullet();
        }

        private void DespawnBullet() => LeanPool.Despawn(gameObject);
    }
}