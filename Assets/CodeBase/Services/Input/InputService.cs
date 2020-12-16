using UnityEngine;

namespace CodeBase.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string HorizontalAxis = "Horizontal";
        protected const string VerticalAxis = "Vertical";
        protected const string FireButton = "Fire";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp() =>
            SimpleInput.GetButtonUp(FireButton);
    }
}
