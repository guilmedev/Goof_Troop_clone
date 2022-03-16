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
            kickableObject.GetComponent<IKickable>().Kick(CalibrateKick(direction), this);
        }
    }
    private Vector2 CalibrateKick(Vector2 direction)
    {
        Vector2 calibratedDirection;

        calibratedDirection.x = (Mathf.RoundToInt(direction.x * 10)) / 10;
        calibratedDirection.y = (Mathf.RoundToInt(direction.y * 10)) / 10;

        //Round to next tenth(decimal)
        //Only accept values 1, -1 or 0 to avoid diagonal movements
        return calibratedDirection;
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
