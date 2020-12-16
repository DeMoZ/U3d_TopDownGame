using CodeBase.CameraLogic;
using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    class HeroMove : MonoBehaviour
    {
        public float MovementSpeed;

        private CharacterController _characterController;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _camera = Camera.main;

            CameraFollow();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * (MovementSpeed * Time.deltaTime));
        }

        private void CameraFollow() =>
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}