using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    class HeroMove : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _jumpCooldown;
        
        private float _jumpCooldownTimer;
        private float _jumpSpeedCurrent;
        private bool _isGrounded;

        private CharacterController _characterController;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = Game.InputService;
            _characterController = GetComponent<CharacterController>();
        }

        private void Update() => 
            Move();

        private void Move()
        {
            var moveVector =  ReadInput(Camera.main.transform);

            RotateTowardsMovement(moveVector);

            moveVector *= _movementSpeed;

            if (_characterController.isGrounded)
                _jumpSpeedCurrent = 0;

            if (CanJump() && _inputService.IsJumpButtonDown())
                _jumpSpeedCurrent = _jumpSpeed;

            _jumpSpeedCurrent += Physics.gravity.y * Time.deltaTime;

            moveVector += _jumpSpeedCurrent * Vector3.up;

            moveVector *= Time.deltaTime;
            moveVector += new Vector3(Physics.gravity.x, 0, Physics.gravity.z) * Time.deltaTime;

            _characterController.Move(moveVector);
        }

        private void RotateTowardsMovement(Vector3 movementVector)
        {
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
                transform.forward = Vector3.Lerp(transform.forward, movementVector, _rotationSpeed * Time.deltaTime);
        }

        private Vector3 ReadInput(Transform referenceTransform)
        {
            var movementVector=Vector3.zero;
            
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = referenceTransform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            return movementVector;
        }

        private bool CanJump()
        {
            _jumpCooldownTimer = (_jumpCooldownTimer > 0) ? _jumpCooldownTimer - Time.deltaTime : 0;

            if (!_isGrounded && _characterController.isGrounded)
                _jumpCooldownTimer = _jumpCooldown;

            _isGrounded = _characterController.isGrounded;

            return _characterController.isGrounded && _jumpCooldownTimer <= 0;
        }
    }
}