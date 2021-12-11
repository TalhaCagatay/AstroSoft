using UnityEngine;

namespace _Game.Scripts.Game.Configs
{
    [DefaultExecutionOrder(-10000)]
    public class GameConfig : MonoBehaviour
    {
        #region Singleton

        public static GameConfig Instance = null;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        
        [SerializeField] private Transform _levelParent;
        [SerializeField] private Transform _bulletsParent;
        [SerializeField] private Transform _asteroidsParent;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _despawnAfterSeconds;
        [SerializeField] private float _playerRespawnTime = 2f;
        [SerializeField] private int _shipMaxLiveCount;
        public Transform LevelParent => _levelParent;
        public Transform BulletsParent => _bulletsParent;
        public Transform AsteroidsParent => _asteroidsParent;
        public Camera MainCamera => _mainCamera;
        public float DespawnAfterSeconds => _despawnAfterSeconds;
        public float PlayerRespawnTime => _playerRespawnTime;
        public int ShipMaxLiveCount => _shipMaxLiveCount;
    }
}