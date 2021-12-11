using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Player.Shooting.Bullet
{
    public class BulletCollisionBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(other.ClosestPoint(transform.position));
                LeanPool.Despawn(gameObject);
            }
        }
    }
}