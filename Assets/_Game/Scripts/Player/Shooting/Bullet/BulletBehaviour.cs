using _Game.Scripts.Configs.BulletConfig;
using UnityEngine;

namespace _Game.Scripts.Player.Shooting.Bullet
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private BulletMovementBehaviour _bulletMovementBehaviour;

        public void Init(BulletConfig bulletConfig) => _bulletMovementBehaviour.Init(bulletConfig);
    }
}