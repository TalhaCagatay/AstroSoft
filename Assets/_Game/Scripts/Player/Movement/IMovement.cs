using _Game.Scripts.Configs.MovementConfig;

namespace _Game.Scripts.Player.Movement
{
    public interface IMovement
    {
        MovementConfig MovementConfig { get; }
        float ThrustSpeed { get; }
        float RotationSpeed { get; }
        void Rotate();
        void Thrust();
    }
}