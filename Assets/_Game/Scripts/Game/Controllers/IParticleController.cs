using UnityEngine;

namespace _Game.Scripts.Game.Controllers
{
    public interface IParticleController : IController
    {
        void SpawnParticleAtPosition(ParticleController.ParticleType particleType, Vector2 worldPosition);
    }
}