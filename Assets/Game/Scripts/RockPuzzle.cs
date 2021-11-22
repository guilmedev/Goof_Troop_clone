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

    private BoxCollider2D[] myColliders;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        //TODO: se não for usar RayCast, pode remover
        myColliders = GetComponents<BoxCollider2D>();
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
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isMoving)
        {
            if (other.gameObject.layer == colliderLayer)
            {
                Stop();
            }
            //TODO: other interface full Damage
        }
    }

    public void SetKickReference(GameObject gameObject)
    {
        // do nothing   
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
