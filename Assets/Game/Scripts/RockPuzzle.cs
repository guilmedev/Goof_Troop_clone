using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Interfaces;
using UnityEngine;

public class RockPuzzle : MonoBehaviour, IKickable
{
    private string playerTag = "Player";

    private IKickBehaviour _guest;

    [SerializeField]
    private bool _avaliableKick;

    private Rigidbody2D _rigidBody;

    private Vector2 myDirection;
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private float m_Speed = 2f;

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
            _rigidBody.MovePosition(_rigidBody.position + myDirection * Time.deltaTime * m_Speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _guest = other.GetComponent<IKickBehaviour>();

        if (_guest != null)
        {
            _avaliableKick = true;
            _guest.SetKickReference(this.gameObject);
        }else
        {
            _avaliableKick = false;
        }        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _guest = other.GetComponent<IKickBehaviour>();
        if (_guest != null)
        {            
            _avaliableKick = false;
            _guest.SetKickReference(null);
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

    public void Kick(Vector2 direction)
    {
        if (_avaliableKick)
        {
            _guest.KickSuccessed();
            PerfomrKick(direction);
        }
    }
    
}
