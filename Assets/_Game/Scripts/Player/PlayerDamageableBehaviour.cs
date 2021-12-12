using System;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerDamageableBehaviour : MonoBehaviour, IDamageable
    {
        public event Action<int> PlayerGotHit;

        private int _health;
        public int Health => _health;

        public void Init(int health) => _health = health;
        public void Dispose(){}

        public void ApplyDamage(Vector2 damagePoint)
        {
            _health--;
            PlayerGotHit?.Invoke(_health);
        }

        private void ApplyDamage()
        {
            _health--;
            PlayerGotHit?.Invoke(_health);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out IDamageable damageable))
                ApplyDamage();
        }
    }
}