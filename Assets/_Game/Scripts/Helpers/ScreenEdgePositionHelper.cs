using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Game.Scripts.Helpers
{
    public class ScreenEdgePositionHelper : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        private static List<Transform> _transformsToHandle = new List<Transform>();
        private float _screenEdgeRight;
        private float _screenEdgeLeft;
        private float _screenEdgeUp;
        private float _screenEdgeDown;
        
        private void Awake()
        {
            Assert.IsNotNull(_mainCamera);
            
            _screenEdgeRight = Screen.width;
            _screenEdgeLeft = 0f;
            _screenEdgeUp = Screen.height;
            _screenEdgeDown = 0f;
        }

        public static void Add(Transform transformToHandle)
        {
            if (_transformsToHandle.Contains(transformToHandle)) return;
            
            _transformsToHandle.Add(transformToHandle);
        }

        public static void Remove(Transform transformToRemove)
        {
            if (!_transformsToHandle.Contains(transformToRemove)) return;
            
            _transformsToHandle.Remove(transformToRemove);
        }
        private void Update()
        {
            for (var i = 0; i < _transformsToHandle.Count; i++)
            {
                var transformToHandle = _transformsToHandle[i];
                var playerScreenPosition = _mainCamera.WorldToScreenPoint(transformToHandle.position);
                if (playerScreenPosition.x < _screenEdgeLeft)
                {
                    playerScreenPosition.x += Screen.width;
                    var worldPosition = _mainCamera.ScreenToWorldPoint(playerScreenPosition);
                    transformToHandle.position = worldPosition;
                }
                if (playerScreenPosition.x > _screenEdgeRight)
                {
                    playerScreenPosition.x -= Screen.width;
                    var worldPosition = _mainCamera.ScreenToWorldPoint(playerScreenPosition);
                    transformToHandle.position = worldPosition;
                }
                if (playerScreenPosition.y > _screenEdgeUp)
                {
                    playerScreenPosition.y -= Screen.height;
                    var worldPosition = _mainCamera.ScreenToWorldPoint(playerScreenPosition);
                    transformToHandle.position = worldPosition;
                }
                if (playerScreenPosition.y < _screenEdgeDown)
                {
                    playerScreenPosition.y += Screen.height;
                    var worldPosition = _mainCamera.ScreenToWorldPoint(playerScreenPosition);
                    transformToHandle.position = worldPosition;
                }
            }
        }
    }
}