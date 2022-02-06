using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Interfaces;
using UnityEngine;

public class RockPuzzle : MonoBehaviour, IKickable
{
    private Rigidbody2D _rigidBody;
    private Vector2 myDirection;
    
    [SerializeField]
    private bool isMoving;

    [SerializeField]
    private float m_Speed = 2f;

    [SerializeField]
    private LayerMask colliderLayer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            if (_rigidBody.velocity.magnitude < .1f)
                Stop();
        }
    }

    private void Stop()
    {
        isMoving = false;
        _rigidBody.isKinematic = true;
    }

    private void PerfomrKick(Vector2 kickDirection)
    {
        _rigidBody.isKinematic = false;
        myDirection = kickDirection;
        _rigidBody.AddForce(myDirection * m_Speed, ForceMode2D.Impulse);

        isMoving = true;
    }

    public void Kick(Vector2 direction, IKickBehaviour kickGuest)
    {
        if (IsValidDestination(direction))
        {
            PerfomrKick(direction);
            kickGuest.KickSuccessed();
        }
        else
        {
            kickGuest.KickFailed();
        }
    }

    private bool IsValidDestination(Vector2 direciton)
    {
        Vector2 end = (Vector2)transform.position + direciton;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direciton, 1f, colliderLayer);

        return hit.collider == null ? true : false;
    }
}
