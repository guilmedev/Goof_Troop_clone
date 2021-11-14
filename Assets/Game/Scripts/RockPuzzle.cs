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

    private void OnTriggerEnter2D(Collider2D other)
    {
        _guest = other.GetComponent<IKickBehaviour>();

        if (_guest != null)
        {
            _avaliableKick = true;
            _guest.SetKickReference(this.gameObject);
        }
        else
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
            if (IsValidDestination(direction))
            {
                _guest.KickSuccessed();
                PerfomrKick(direction);
            }
            else
            {
                _guest.KickFailed();
            }
        }
    }
    private bool IsValidDestination(Vector2 direciton)
    {

        return true;
        //TODO:
        /*
         - Tentar validar via TileMap (checar se tem collider na posição desejada)
            RayCast não funcionou pq ele acusa o proprio collider, e se desativar os colliders para fazer o ray,
            perde a funcionalidade das interfaces e o IKickable perde a referencia por causa do OnTriggerExit
        */

        // return true;

        // Cast a ray straight down.
        Vector2 end = (Vector2)transform.position + direciton;
        foreach (BoxCollider2D colliders in myColliders)
        {
            colliders.enabled = false;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direciton, 1.2f);

        foreach (BoxCollider2D colliders in myColliders)
        {
            colliders.enabled = true;
        }
        
        if (hit.collider != null)
        {
            return true;
        }
        else
        {            
            return true;
        }
    }
}
