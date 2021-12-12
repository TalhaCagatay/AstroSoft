using System;
using UnityEngine; //using BayatGames.SaveGameFree;

namespace _Game.Scripts.Game.Controllers
{
    public class PrefsController : IPrefsController
    {
        public event Action Initialized;
        public event Action Disposed;
        public event Action<int> HighScoreChanged;
        
        private bool _isInitialized;
        
        public bool IsInitialized => _isInitialized;
        
        private const string HIGH_SCORE_KEY = "save-key-earning-upgrade";

        public void Init()
        {
            _isInitialized = true;
            Initialized?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Initialized");
        }

        public void SetHighScore(int newHighScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, newHighScore);
            HighScoreChanged?.Invoke(newHighScore);
        }

        public int GetHighScore() => PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        
        public void Dispose()
        {
            _isInitialized = false;
            Disposed?.Invoke();
            Debug.Log($"{LOGS.HEAD_LOG} {this} Disposed");
        }
    }
}