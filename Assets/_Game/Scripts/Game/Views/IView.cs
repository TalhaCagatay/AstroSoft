using System;
using _Game.Scripts.Game.Levels;

namespace _Game.Scripts.Game.Views
{
    public interface IView
    {
        event Action<IView> ViewOpened;
        event Action<IView> ViewClosed;
        ILevel Level { get; set; }
        void Init();
        void Open();
        void Close();
    }
}