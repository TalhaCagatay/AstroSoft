using _Game.Scripts.Configs.BulletConfig;

namespace _Game.Scripts.Player.Shooting
{
    public interface IShoot
    {
        BulletConfig BulletConfig { get; }
        void Shoot();
    }
}