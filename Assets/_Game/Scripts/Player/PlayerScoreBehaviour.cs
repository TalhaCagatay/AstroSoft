using _Game.Scripts.Asteroid;
using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerScoreBehaviour : MonoBehaviour
    {
        private static int _currentScore;
        public static int CurrentScore => _currentScore;
        
        public void Init()
        {
            ResetScore();
            GameController.Instance.StateChanged += OnStateChanged;
            AsteroidBehaviour.AsteroidDestroyed += OnAsteroidDestroyed;
        }

        private static void ResetScore()
        {
            _currentScore = 0;
        }

        public void Dispose()
        {
            GameController.Instance.StateChanged -= OnStateChanged;
            AsteroidBehaviour.AsteroidDestroyed -= OnAsteroidDestroyed;
        }

        private void OnStateChanged(GameState newState)
        {
            if (newState == GameState.Play)
                ResetScore();
        }

        private void OnAsteroidDestroyed(int asteroidScore)
        {
            _currentScore += asteroidScore;
            if(_currentScore > GameController.Instance.PrefsController.GetHighScore())
                GameController.Instance.PrefsController.SetHighScore(_currentScore);
        }
    }
}