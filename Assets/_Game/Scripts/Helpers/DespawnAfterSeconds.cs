using _Game.Scripts.Game.Controllers;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Helpers
{
    /// <summary>
    /// This class is responsible of despawning gameobjects after the defined time in the gameconfig passes 
    /// </summary>
    public class DespawnAfterSeconds : MonoBehaviour
    {
        private float _despawnAfterSeconds;

        private void OnEnable()
        {
            GetDespawningTime();
            Invoke("Despawn", _despawnAfterSeconds);
        }

        private void GetDespawningTime() => _despawnAfterSeconds = GameController.Instance.GameConfig.DespawnAfterSeconds;

        private void OnDisable() => CancelInvoke("Despawn");

        private void Despawn() => LeanPool.Despawn(gameObject);
    }
}