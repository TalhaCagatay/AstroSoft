using System;

namespace _Game.Scripts.Game.Views
{
    public interface IView
    {
        event Action<IView> ViewOpened;
        event Action<IView> ViewClosed;
        void Init();
        void Open();
        void Close();
    }
}