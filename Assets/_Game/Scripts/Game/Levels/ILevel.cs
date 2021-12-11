namespace _Game.Scripts.Game.Levels
{
    public interface ILevel
    {
        int LevelId { get; }
        void Init();
        void Dispose();
    }
}