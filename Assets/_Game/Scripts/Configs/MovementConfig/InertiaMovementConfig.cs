using UnityEngine;

namespace _Game.Scripts.Configs.MovementConfig
{
    [CreateAssetMenu(fileName = "InertiaMovementConfig", menuName = "AstroSoft/MovementConfigs/InertiaMovementConfig", order = 1)]
    public class InertiaMovementConfig : MovementConfig
    {
        public float ThrustAcceleration = 1f;
        public float ThrustDeacceleration = 1f;
        public float MaxSpeed = 3f;
    }
}