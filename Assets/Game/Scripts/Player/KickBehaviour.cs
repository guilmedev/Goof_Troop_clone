using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Interfaces;
using UnityEngine;

public class KickBehaviour : MonoBehaviour, IKickBehaviour
{

    public Action OnKickSuccess;
    public Action OnKickFail;

    public void DoKick(Vector2 direction, GameObject kickableObject)
    {
        if (kickableObject != null)
        {
            if (direction.x != 0 && direction.y != 0) return;

            kickableObject.GetComponent<IKickable>().Kick(direction, this);
        }
    }

    public void KickFailed()
    {
        OnKickFail?.Invoke();
    }

    public void KickSuccessed()
    {
        OnKickSuccess?.Invoke();
    }
}
