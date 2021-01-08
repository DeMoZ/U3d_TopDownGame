using System;
using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class BotMove : MonoBehaviour, IMove
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        private event Action<Vector3, float> OnMoveVectorCalculated;

        private CharacterController _characterController;
        private IInputService _inputService;
        private Camera _camera;
      
        private void Awake()
        {
            _inputService = Game.InputService;
            _characterController = GetComponent<CharacterController>();
            _camera = Camera.main;
        }

        private void Update() =>
            Move();

        private bool _signed;

        private void Move()
        {
            var moveVector = ReadInput(_camera.transform);
            RotateTowardsMovement(moveVector);
            moveVector *= _movementSpeed * Time.deltaTime;
            
            _signed = OnMoveVectorCalculated != null;

            if (OnMoveVectorCalculated == null)
                _characterController.Move(moveVector);
            else
                OnMoveVectorCalculated.Invoke(moveVector, Time.deltaTime);
        }

        private void RotateTowardsMovement(Vector3 movementVector)
        {
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
                transform.forward = Vector3.Lerp(transform.forward, movementVector, _rotationSpeed * Time.deltaTime);
        }

        private Vector3 ReadInput(Transform referenceTransform)
        {
            var movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = referenceTransform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            return movementVector;
        }

        public void SubscribeOnMovement(Action<Vector3, float> callback) =>
            OnMoveVectorCalculated += callback;

        public void UnSubscribeOnMovement(Action<Vector3, float> callback) =>
            OnMoveVectorCalculated -= callback;
    }
}