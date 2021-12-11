using System;

namespace _Game.Scripts.Game.Controllers
{
    public interface IPlayerController : IController
    {
        event Action<int> PlayerLostLive;
        event Action PlayerDied;
    }
}