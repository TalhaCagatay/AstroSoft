using UnityEngine;

namespace _Game.Scripts.Configs.MovementConfig
{
    [CreateAssetMenu(fileName = "BaseMovementConfig", menuName = "AstroSoft/MovementConfigs/BaseMovementConfig", order = 0)]
    public class MovementConfig : ScriptableObject
    {
        public float ThrustSpeed;
        public float RotationSpeed;
    }
}