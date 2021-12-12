using System;
using _Game.Scripts.Game.Sound;
using UnityEngine;

namespace _Game.Scripts.Game.Controllers
{
    [RequireComponent(typeof(SoundBehaviour))]
    public class SoundController : MonoBehaviour, ISoundController
    {
        public enum SoundType
        {
            BackgroundMusic,
            Shot,
            Explosion,
            Death,
            Respawn
        }
     
        public event Action Initialized;
        public event Action Disposed;

        private SoundBehaviour _soundBehaviour;
        private SoundCallbackBridge _soundCallbackBridge;
        private GameObject _oneShotGameObject;
        private AudioSource _oneShotAudioSource;
        
        private bool _isInitialized;
        public bool IsInitialized => _isInitialized;
        public void Init()
        {
            CreateSoundCallbackBridge();
            GetSoundBehaviour();
            _isInitialized = true;
            Initialized?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Initialized");
        }

        private void GetSoundBehaviour() => _soundBehaviour = GetComponent<SoundBehaviour>();

        private void CreateSoundCallbackBridge()
        {
            _soundCallbackBridge = new SoundCallbackBridge();
            _soundCallbackBridge.Init();
        }

        public void PlaySound(SoundType soundType)
        {
            if (_oneShotGameObject == null)
            {
                _oneShotGameObject = new GameObject(soundType.ToString());
                _oneShotGameObject.transform.SetParent(GameController.Instance.GameConfigMono.SoundsParent);
                _oneShotAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
                _oneShotAudioSource.volume = GameController.Instance.GameConfig.SoundVolume;
            }
            _oneShotAudioSource.PlayOneShot(GetAudioClip(soundType));
        }

        private AudioClip GetAudioClip(SoundType soundType)
        {
            foreach (var soundAudioClip in _soundBehaviour.SoundAudioClips)
                if (soundAudioClip.SoundType == soundType)
                    return soundAudioClip.AudioClip;

            Debug.LogError($"{LOGS.HEAD_LOG} {this} SoundType:{soundType} not found");
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