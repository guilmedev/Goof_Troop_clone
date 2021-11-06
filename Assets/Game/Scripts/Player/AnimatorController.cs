using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("VerticalIdle", _playerController.IdleDirection.y);
        _animator.SetFloat("HorizontalIdle", _playerController.IdleDirection.x);


        _animator.SetFloat("InputMagnitude", _playerController.PlayerVelocity);
        _animator.SetFloat("VerticalInput", _playerController.PlayerInput.y);
        _animator.SetFloat("HorizontalInput", _playerController.PlayerInput.x);

    }
}
