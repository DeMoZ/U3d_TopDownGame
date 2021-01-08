using System;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(IMove))]
    public class BotMoveUseGravity : MonoBehaviour
    {
        private IMove _move;
        private CharacterController _characterController;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _move = GetComponent<IMove>();
            _move.SubscribeOnMovement(UpdateJump);
        }

        private void OnDestroy() =>
            _move.UnSubscribeOnMovement(UpdateJump);

        private void UpdateJump(Vector3 moveVector, float deltaTime)
        {
            moveVector.y += Physics.gravity.y * deltaTime;
            _characterController.Move(moveVector);

            if (_characterController.isGrounded)
                moveVector.y = 0;
        }
    }
}