using UnityEngine;

namespace _Game.Scripts.Game.Levels
{
    public class LevelMultiplierBehaviour : MonoBehaviour
    {
        [SerializeField] private int _multiplier;
        public int Multiplier => _multiplier;
    }
}