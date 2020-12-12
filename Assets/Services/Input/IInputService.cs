using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        Vector2 Axis();
        bool FireButtonUp();
    }

    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string FireButton = "Fire";
        
        public abstract Vector2 Axis();

        public bool FireButtonUp() => 
            SimpleInput.GetButtonUp(FireButton);

        protected static Vector2 GetAxisSimpleInput() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}