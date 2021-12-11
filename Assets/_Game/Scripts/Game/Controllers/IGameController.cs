namespace _Game.Scripts.Game.Controllers
{
    public interface IGameController : IController
    {
        GameState GameState { get; }
        IPlayerController PlayerController { get; set; }
        IPrefsController PrefsController { get; set; }
        IViewController ViewController { get; }
        IAsteroidController AsteroidController { get; }
    }
}