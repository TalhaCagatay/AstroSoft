using _Game.Scripts.Game.Configs;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Helpers
{
    public class DespawnAfterSeconds : MonoBehaviour
    {
        private float _despawnAfterSeconds;

        private void OnEnable()
        {
            _despawnAfterSeconds = GameConfig.Instance.DespawnAfterSeconds;
            Invoke("Despawn", _despawnAfterSeconds);
        }

        private void OnDisable() => CancelInvoke("Despawn");

        private void Despawn() => LeanPool.Despawn(gameObject);
    }
}