using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameActions actions;

    public Action<Vector2> OnPlayerMove;

    [SerializeField]
    private float _speed;

    public float Speed => _speed;
    

    private void Awake() 
    {
        //Init
        actions = new GameActions();    
    }
    private void OnEnable() 
    {
        actions.Enable();
    }
    private void OnDisable() 
    {
        actions.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        // actions.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        actions.Player.Fire.performed += ctx => Use();
    }

    private void Use()
    {
        //TODO: change controlls
        Debug.Log("Use");
    }

    public Vector2 GetMovement()
    {
        return actions.Player.Move.ReadValue<Vector2>();
    }
}
