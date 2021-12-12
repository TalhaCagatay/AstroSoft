using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Helpers
{
    public class EasyRotate : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationEndValue;
        [SerializeField][Tooltip("-1 for infinite")] private int _loopAmount = -1;
        [SerializeField] private LoopType _loopType = LoopType.Yoyo;
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private float _durationMin = 1f;
        [SerializeField] private float _durationMax = 10f;

        private void OnEnable() => transform.DORotate(_rotationEndValue, Random.Range(_durationMin, _durationMax)).SetLoops(_loopAmount, _loopType).SetRelative().SetEase(_ease);
    }
}