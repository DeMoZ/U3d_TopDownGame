﻿using UnityEngine;

namespace Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis()
        {
            Vector2 axis = GetAxisSimpleInput();

            if (axis == Vector2.zero)
                axis = UnityAxis();

            return axis;
        }

        private static Vector2 UnityAxis() =>
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}