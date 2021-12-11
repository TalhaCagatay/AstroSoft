using _Game.Scripts.Configs.BulletConfig;
using UnityEngine;

namespace _Game.Scripts.Player.Shooting.Bullet
{
    public class BulletMovementBehaviour : MonoBehaviour
    {
        private BulletConfig _bulletConfig;
        
        public void Init(BulletConfig bulletConfig) => _bulletConfig = bulletConfig;

        private void Update() => transform.Translate(Vector3.up * (_bulletConfig.BulletSpeed * Time.deltaTime),Space.Self);
    }
}