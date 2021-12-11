using _Game.Scripts.Configs.BulletConfig;
using _Game.Scripts.Game.Configs;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Player.Shooting
{
    public class PlayerShootBehaviour : MonoBehaviour, IShoot
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Transform _bulletSpawnTransform;
        private float _lastShootTime = 0f;

        public BulletConfig BulletConfig => _bulletConfig;
        
        private void Update()
        {
            _lastShootTime += Time.deltaTime;
            if (Input.GetKey(KeyCode.Space) && CanShoot())
                Shoot();
        }

        public void Shoot()
        {
            var spawnedBulletBehaviour = LeanPool.Spawn(_bulletConfig.BulletBehaviour, GameConfig.Instance.BulletsParent);
            var cachedTransform = spawnedBulletBehaviour.transform;
            cachedTransform.position = _bulletSpawnTransform.position;
            cachedTransform.rotation = _bulletSpawnTransform.rotation;
            spawnedBulletBehaviour.Init(_bulletConfig);
            ResetShootingTime();
        }

        private void ResetShootingTime()
        {
            _lastShootTime = 0f;
        }

        private bool CanShoot() => _lastShootTime >= _bulletConfig.FiringFrequencySeconds;
    }
}