using System;
using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Game.Particle
{
    public class ParticleBehaviour : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _shootParticle;
        [SerializeField] private ParticleSystem _explosionParticle;
        [SerializeField] private Particle[] _particles;

        public Particle[] Particles => _particles;
    }
    
    [Serializable]
    public class Particle
    {
        public ParticleController.ParticleType ParticleType;
        public ParticleSystem ParticleItself;
    }
}