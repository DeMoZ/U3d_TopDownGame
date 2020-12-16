﻿using UnityEngine;

namespace CodeBase.Services.Input
{
    public class StandaloneInpurtSevice : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis;

                if (axis == Vector2.zero)
                    axis = UnityAxis;

                return axis;
            }
        }

        private static Vector2 UnityAxis => new Vector2(UnityEngine.Input.GetAxis(HorizontalAxis), UnityEngine.Input.GetAxis(VerticalAxis));

        private static Vector2 SimpleInputAxis => new Vector2(SimpleInput.GetAxis(HorizontalAxis), SimpleInput.GetAxis(VerticalAxis));
    }
}
