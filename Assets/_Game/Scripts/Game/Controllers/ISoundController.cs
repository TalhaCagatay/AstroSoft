namespace _Game.Scripts.Game.Controllers
{
    public interface ISoundController : IController
    {
        void PlaySound(SoundController.SoundType soundType);
    }
}