using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace JobsMoveToClickPoint
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    
    public class BotAnimator :MonoBehaviour, IAnimator
    {
        // Animator properties
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int MoveForward = Animator.StringToHash("MoveForward");
        private static readonly int MoveRight = Animator.StringToHash("MoveRight");
        private static readonly int Jump = Animator.StringToHash("Jump");

        // Animator states
        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _walkingStateHash = Animator.StringToHash("move");

        private Animator _animator;
        private CharacterController _characterController;

        public bool IsMovingMark;

        private IInputService _inputService;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _inputService = Game.InputService;
        }

        private void Update()
        {
            var velocity = _characterController.velocity;
            velocity = _characterController.transform.InverseTransformVector(velocity);
            ApplyMovement(velocity);
        }

        public void ApplyJump()
        {
//            Debug.Log("ApplyJump");
//            if (velocity.magnitude > 0.1f && velocity.magnitude < 0.5f)
//            {
//            }
//            else if (velocity.magnitude > 0.1f)
//            {
//            }
//            else
//            {
//                _animator.SetTrigger(Jump);
//            }
            
            _animator.SetTrigger(Jump);

        }

        private void ApplyMovement(Vector3 velocity)
        {
            if (velocity.magnitude > 0.1f)
                _animator.SetBool(Move, true);
            else
                _animator.SetBool(Move, false);

            _animator.SetFloat(MoveForward, velocity.z);
            _animator.SetFloat(MoveRight, velocity.x);

            IsMovingMark = velocity.magnitude > 0.1f;
        }
    }
}