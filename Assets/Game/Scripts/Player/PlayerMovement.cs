using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameActions actions;

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
        actions.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        actions.Player.Fire.performed += ctx => Use();
    }

    private void Use()
    {
        //TODO: change controlls
        Debug.Log("Use");
    }

    private void Move(Vector2 direction)
    {
        
        Debug.Log(direction);
    }
}
