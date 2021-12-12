using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Helpers
{
    /// <summary>
    /// This class is responsible of despawning gameobjects to the pool when they disable
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class DespawnParticleOnCallback : MonoBehaviour
    {
        private void OnParticleSystemStopped() => LeanPool.Despawn(gameObject);
    }
}