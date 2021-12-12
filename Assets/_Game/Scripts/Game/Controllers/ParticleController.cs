using System;
using _Game.Scripts.Game.Particle;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Game.Controllers
{
    [RequireComponent(typeof(ParticleBehaviour))]
    public class ParticleController : MonoBehaviour, IParticleController
    {
        public enum ParticleType
        {
            Shoot,
            Explosion
        }
        
        public event Action Initialized;
        public event Action Disposed;

        private ParticleBehaviour _particleBehaviour;
        private ParticleCallbackBridge _particleCallbackBridge;
        
        private bool _isInitialized;
        public bool IsInitialized => _isInitialized;
        public void Init()
        {
            CreateParticleCallbackBridge();
            GetParticleBehaviour();
            _isInitialized = true;
            Initialized?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Initialized");
        }

        private void GetParticleBehaviour() => _particleBehaviour = GetComponent<ParticleBehaviour>();

        private void CreateParticleCallbackBridge()
        {
            _particleCallbackBridge = new ParticleCallbackBridge();
            _particleCallbackBridge.Init();
        }

        public void SpawnParticleAtPosition(ParticleType particleType, Vector2 worldPosition)
        {
            ParticleSystem particle = ResolveParticleType(particleType);
            LeanPool.Spawn(particle, worldPosition, Quaternion.identity,
                GameController.Instance.GameConfigMono.ParticlesParent);
        }

        private ParticleSystem ResolveParticleType(ParticleType particleType)
        {
            foreach (var particle in _particleBehaviour.Particles)
                if (particle.ParticleType == particleType)
                    return particle.ParticleItself;
            
            Debug.LogError($"{LOGS.HEAD_LOG} {this} ParticleType:{particleType} not found");
            return null;
        }

        public void Dispose()
        {
            _isInitialized = false;
            Disposed?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Disposed");
        }
    }
}