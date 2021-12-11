using _Game.Scripts.Player.Shooting.Bullet;
using UnityEngine;

namespace _Game.Scripts.Configs.BulletConfig
{
    [CreateAssetMenu(fileName = "BaseBulletConfig", menuName = "AstroSoft/BulletConfig/BaseBulletConfig", order = 1)]
    public class BulletConfig : ScriptableObject
    {
        public BulletBehaviour BulletBehaviour;
        public float BulletSpeed;
        public float FiringFrequencySeconds;
    }
}