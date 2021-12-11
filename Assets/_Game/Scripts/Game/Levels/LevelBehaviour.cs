using UnityEngine;

namespace _Game.Scripts.Game.Levels
{
    public class LevelBehaviour : MonoBehaviour, ILevel
    {
        [SerializeField] private int _levelId;
        public int LevelId => _levelId;
        
        public virtual void Init() => Debug.Log($"{LOGS.HEAD_LOG} Level ID:{_levelId} initialized");

        public virtual void Dispose() => Debug.Log($"{LOGS.HEAD_LOG} Level ID:{_levelId} disposed");
    }
}