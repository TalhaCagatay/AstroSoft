using System;
using _Game.Scripts.Configs.MovementConfig;
using _Game.Scripts.Game.Controllers;
using _Game.Scripts.Helpers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Game.Scripts.Player.Movement
{
    public class PlayerMovementBehaviour : MonoBehaviour, IMovement
    {
        [SerializeField] private InertiaMovementConfig _inertiaMovementConfig;
        
        private Transform _transformToHandleMovement;
        private Rigidbody2D _shipRigidbody2D;
        private bool _isThrusting;
        
        public MovementConfig MovementConfig => _inertiaMovementConfig;
        public float ThrustSpeed => MovementConfig.ThrustSpeed;
        public float RotationSpeed => MovementConfig.RotationSpeed;
        
        public void Init()
        {
            PositionPlayerAtCenter();
            GetTransformToHandle();
            GetShipRigidbody2D();
        }

        public void Dispose() => ResetShipVelocity();

        private void OnDisable() => _isThrusting = false;

        public void ResetShipVelocity() => _shipRigidbody2D.velocity = Vector2.zero;

        private void GetShipRigidbody2D() => _shipRigidbody2D = ((PlayerController) GameController.Instance.PlayerController).ShipRigidbody2D;

        private void GetTransformToHandle() => _transformToHandleMovement = ((PlayerController) GameController.Instance.PlayerController).TransformToMove;

        private void PositionPlayerAtCenter() => transform.position = Vector3.zero;

        private void Update()
        {
            Rotate();
            _isThrusting = IsThrusting();
        }

        private void FixedUpdate()
        {
            if (!_isThrusting)
                _shipRigidbody2D.drag = _inertiaMovementConfig.ThrustDeacceleration;
            else
                Thrust();
        }

        private bool IsThrusting()
        {
#if UNITY_EDITOR
            return Input.GetKey(KeyCode.UpArrow);
#else
            return TouchHelper.JoyStickVector.y > 0f;
#endif

        }

        public void Rotate()
        {
#if UNITY_EDITOR
            if (InputHelper.SwipeMoveDirection == InputHelper.SwipeDirection.Left)
                _transformToHandleMovement.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime), Space.Self);
            if (InputHelper.SwipeMoveDirection == InputHelper.SwipeDirection.Right)
                _transformToHandleMovement.Rotate(Vector3.forward * (-RotationSpeed * Time.deltaTime), Space.Self);      
#else
            _transformToHandleMovement.Rotate(Vector3.forward * (-TouchHelper.JoyStickVector.x * (RotationSpeed * Time.deltaTime)), Space.Self);
#endif
            
        }

        public void Thrust()
        {
            _shipRigidbody2D.drag = 0f;
            _shipRigidbody2D.AddForce(_transformToHandleMovement.up * _inertiaMovementConfig.ThrustAcceleration, ForceMode2D.Force);
            _shipRigidbody2D.velocity = Vector2.ClampMagnitude(_shipRigidbody2D.velocity, _inertiaMovementConfig.MaxSpeed);
        }
    }
}