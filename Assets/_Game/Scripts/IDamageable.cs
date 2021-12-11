using UnityEngine;

namespace _Game.Scripts
{
    public interface IDamageable : IHealth
    {
        void ApplyDamage(Vector2 damagePoint);
    }
}