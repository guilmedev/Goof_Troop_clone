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
    [SerializeField]
    private float _rayCastHitColliderDistance = .52f;



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
            _rigidBody.MovePosition(_rigidBody.position + myDirection * Time.deltaTime * m_Speed);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, myDirection, _rayCastHitColliderDistance, colliderLayer);
            if (hit.collider != null)
            {
                Stop();
            }
        }
        // Debug.DrawRay(transform.position, myDirection * _rayCastHitColliderDistance, Color.green);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isMoving)
        {
            //TODO: other interface full Damage
        }
    }

    private void Stop()
    {
        isMoving = false;
        _rigidBody.isKinematic = true;
        myDirection = Vector2.zero;
    }

    private void PerfomrKick(Vector2 kickDirection)
    {
        _rigidBody.isKinematic = false;
        myDirection = kickDirection;
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
