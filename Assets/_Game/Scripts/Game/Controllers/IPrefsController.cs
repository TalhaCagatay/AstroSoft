using System;

namespace _Game.Scripts.Game.Controllers
{
    public interface IPrefsController : IController
    {
        event Action<int> HighScoreChanged;
        void SetHighScore(int newHighScore);
        int GetHighScore();
    }
}