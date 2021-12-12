using System;
using _Game.Scripts.Configs.BulletConfig;
using _Game.Scripts.Game.Controllers;
using _Game.Scripts.MobileInput;
using _Game.Scripts.Player.Shooting.Bullet;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Player.Shooting
{
    public class PlayerShootBehaviour : MonoBehaviour, IShoot
    {
        public static event Action PlayerShoot;
        
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Transform _bulletSpawnTransform;
        private float _lastShootTime;

        public BulletConfig BulletConfig => _bulletConfig;
        
        private void Update() => HandleShooting();

        private void HandleShooting()
        {
            _lastShootTime += Time.deltaTime;
            if (IsFiring() && CanShoot())
                Shoot();
        }

        private static bool IsFiring() => Input.GetKey(KeyCode.Space) || JoyStickView.Firing;

        public void Shoot()
        {
            var spawnedBulletBehaviour = SpawnBulletBehaviour();
            var cachedTransform = spawnedBulletBehaviour.transform;
            SetBulletPosition(cachedTransform);
            SetBulletRotation(cachedTransform);
            InitializeBulletBehaviour(spawnedBulletBehaviour);
            ResetShootingTime();
            PlayerShoot?.Invoke();
        }

        private void InitializeBulletBehaviour(BulletBehaviour spawnedBulletBehaviour) => spawnedBulletBehaviour.Init(_bulletConfig);

        private void SetBulletRotation(Transform cachedTransform) => cachedTransform.rotation = _bulletSpawnTransform.rotation;

        private void SetBulletPosition(Transform cachedTransform) => cachedTransform.position = _bulletSpawnTransform.position;

        private BulletBehaviour SpawnBulletBehaviour() => LeanPool.Spawn(_bulletConfig.BulletBehaviour, GameController.Instance.GameConfigMono.BulletsParent);

        private void ResetShootingTime() => _lastShootTime = 0f;

        private bool CanShoot() => _lastShootTime >= _bulletConfig.FiringFrequencySeconds;
    }
}