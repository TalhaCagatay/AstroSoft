using System;
using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Game.Sound
{
    public class SoundBehaviour : MonoBehaviour
    {
        [SerializeField] private AudioClip _backgroundMusic;
        [SerializeField] private AudioClip _shotClip;
        [SerializeField] private AudioClip _explosionClip;
        [SerializeField] private AudioClip _deathClip;
        [SerializeField] private AudioClip _respawnClip;
        [SerializeField] private SoundAudioClip[] _soundAudioClips;
        public SoundAudioClip[] SoundAudioClips => _soundAudioClips;
    }

    [Serializable]
    public class SoundAudioClip
    {
        public SoundController.SoundType SoundType;
        public AudioClip AudioClip;
    }
}