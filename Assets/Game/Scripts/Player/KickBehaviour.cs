using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Interfaces;
using UnityEngine;

public class KickBehaviour : MonoBehaviour , IKickBehaviour
{

    public Action onKickSuccess;
    public Action onKickFail;

    private GameObject _kickableObject;

    public void DoKick(Vector2 direction)
    {
        if (_kickableObject != null)
        {
            _kickableObject.GetComponent<IKickable>().Kick(direction);
        }
    }

    public void KickFailed()
    {
        onKickFail?.Invoke();
    }

    public void KickSuccessed()
    {
        onKickSuccess?.Invoke();
    }

    public void SetKickReference(GameObject obj)
    {
        _kickableObject = obj;
    }
}
