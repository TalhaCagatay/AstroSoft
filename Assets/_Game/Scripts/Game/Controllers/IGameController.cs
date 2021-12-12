using _Game.Scripts.Configs.GameConfig;

namespace _Game.Scripts.Game.Controllers
{
    public interface IGameController : IController
    {
        GameState GameState { get; }
        IPlayerController PlayerController { get; set; }
        IPrefsController PrefsController { get; set; }
        IViewController ViewController { get; }
        IAsteroidController AsteroidController { get; }
        ISoundController SoundController { get; }
        IParticleController ParticleController { get; }
        GameConfig GameConfig { get; }
        GameConfigMono GameConfigMono { get; }
    }
}