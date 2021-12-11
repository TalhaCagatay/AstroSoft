using _Game.Scripts.Helpers;
using UnityEngine;

namespace _Game.Scripts
{
    public class PositionCrossScreenEdgeBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _transformToHandle;

        private void OnEnable() => ScreenEdgePositionHelper.Add(_transformToHandle);
        private void OnDisable() => ScreenEdgePositionHelper.Remove(_transformToHandle);
    }
}