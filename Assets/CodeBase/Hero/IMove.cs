using UnityEngine;
using System;

namespace CodeBase.Hero
{
    internal interface IMove
    {
        void SubscribeOnMovement(Action<Vector3> callback);
        void UnSubscribeOnMovement(Action<Vector3> callback);
    }
}