using UnityEngine;

namespace CodeBase.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => new Vector2(SimpleInput.GetAxis(HorizontalAxis), SimpleInput.GetAxis(VerticalAxis));
    }
}
