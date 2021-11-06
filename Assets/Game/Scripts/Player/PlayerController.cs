using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Start()
    {

    }

    public float PlayerVelocity => _playerMovement.GetMovement().magnitude;
    public Vector2 PlayerInput => _playerMovement.GetMovement();
    private Vector2 _idleDirection;
    public Vector2 IdleDirection => _idleDirection;

    private void FixedUpdate()
    {
        //TODO: verify glith when stops
        if (_playerMovement.GetMovement().x != 0 || _playerMovement.GetMovement().y != 0)
        {
            _idleDirection = _playerMovement.GetMovement();
        }
        _rigidbody2D.velocity = _playerMovement.GetMovement() * _playerMovement.Speed;
    }
}
