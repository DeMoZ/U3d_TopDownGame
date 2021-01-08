using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using JobsMoveToClickPoint;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(IMove))]
    [RequireComponent(typeof(IAnimator))]
    [RequireComponent(typeof(CharacterController))]
    public class BotJump : MonoBehaviour
    {
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _jumpCooldown;

        private IMove _move;
        private IAnimator _animator;
        private IInputService _inputService;
        private CharacterController _characterController;

        private float _jumpCooldownTimer;
        private float _jumpSpeedCurrent;
        private bool _isGrounded;

        private void Awake()
        {
            _move = GetComponent<IMove>();
            _animator = GetComponent<IAnimator>();
            _inputService = Game.InputService;
            _characterController = GetComponent<CharacterController>();

            _move.SubscribeOnMovement(UpdateJump);
        }

        private void OnDestroy() =>
            _move.UnSubscribeOnMovement(UpdateJump);

      private void UpdateJump(Vector3 moveVector, float deltaTime)
        {
            if (_characterController.isGrounded)
                _jumpSpeedCurrent = 0;

            var sendJump = false;

            if (CanJump() && _inputService.IsJumpButtonDown())
            {
                _jumpSpeedCurrent = _jumpSpeed;
                sendJump = true;
            }

            _jumpSpeedCurrent += Physics.gravity.y * Time.deltaTime;

            moveVector.y = _jumpSpeedCurrent * Time.deltaTime;

            _characterController.Move(moveVector);

            if (sendJump)
                _animator.ApplyJump(moveVector);
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