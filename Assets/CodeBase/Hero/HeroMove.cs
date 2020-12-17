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

        private void Awake()
        {
            _inputService = Game.InputService;
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * (MovementSpeed * Time.deltaTime));
        }
    }
}