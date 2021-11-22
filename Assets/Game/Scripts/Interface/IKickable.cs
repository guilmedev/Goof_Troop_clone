using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IKickable
    {
        void Kick(Vector2 direciton, IKickBehaviour kickGuest);
    }
}