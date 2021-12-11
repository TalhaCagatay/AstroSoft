using System;

namespace _Game.Scripts.Game.Controllers
{
    public interface IController
    {
        event Action Initialized;
        event Action Disposed;
        bool IsInitialized { get; }
        void Init();
        void Dispose();
    }
}