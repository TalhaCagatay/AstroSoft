using _Game.Scripts.Game.Controllers;
using _Game.Scripts.Helpers;
using UnityEngine;

namespace _Game.Scripts
{
    public class PositionCrossScreenEdgeBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _transformToHandle;
        private bool _addedToList;

        private void AddToScreenEdgePositionHelper() => ScreenEdgePositionHelper.Add(_transformToHandle);

        private void Update()
        {
            if (_addedToList) return;
            
            var positionView = CalculateViewPosition();
            if(IsInsideView(positionView))
            {
                AddToScreenEdgePositionHelper();
                _addedToList = true;
            }
        }

        private Vector3 CalculateViewPosition() => GameController.Instance.GameConfigMono.MainCamera.WorldToViewportPoint(_transformToHandle.position);

        private static bool IsInsideView(Vector3 positionView) => positionView.x > 0 && positionView.x < 1 && positionView.y > 0 && positionView.y < 1;

        private void OnDisable()
        {
            ScreenEdgePositionHelper.Remove(_transformToHandle);
            _addedToList = false;
        }
    }
}