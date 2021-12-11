using _Game.Scripts.Configs.MovementConfig;
using _Game.Scripts.Game.Controllers;
using _Game.Scripts.Helpers;
using UnityEngine;
using UnityEngine.Assertions;
using Vector3 = UnityEngine.Vector3;

namespace _Game.Scripts.Player.Movement
{
    public class PlayerMovementBehaviour : MonoBehaviour, IMovement
    {
        private Transform _transformToHandleMovement;

        [SerializeField] private InertiaMovementConfig _inertiaMovementConfig;

        public MovementConfig MovementConfig => _inertiaMovementConfig;
        public float ThrustSpeed => MovementConfig.ThrustSpeed;
        public float RotationSpeed => MovementConfig.RotationSpeed;
        
        private void Awake() => Assert.IsNotNull(_inertiaMovementConfig);

        public void Init()
        {
            PositionPlayerAtCenter();
            GetTransformToHandle();
        }

        private void GetTransformToHandle() => _transformToHandleMovement = ((PlayerController) GameController.Instance.PlayerController).TransformToMove;

        private void PositionPlayerAtCenter() => transform.position = Vector3.zero;

        private void Update()
        {
            Rotate();
            Thrust();
        }

        public void Rotate()
        {
            if (TouchHelper.SwipeMoveDirection == TouchHelper.SwipeDirection.Left)
                _transformToHandleMovement.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime), Space.Self);
            if (TouchHelper.SwipeMoveDirection == TouchHelper.SwipeDirection.Right)
                _transformToHandleMovement.Rotate(Vector3.forward * (-RotationSpeed * Time.deltaTime), Space.Self);
        }

        public void Thrust()
        {
            if (Input.GetKey(KeyCode.UpArrow))
                _transformToHandleMovement.Translate(Vector3.up * (ThrustSpeed * Time.deltaTime),Space.Self);
        }
    }
}