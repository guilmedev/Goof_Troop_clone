using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private GameActions actions;

        public Action OnPlayerClick;

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
            actions.Player.Kick.performed += ctx => Use();
        }

        private void Use()
        {
            OnPlayerClick?.Invoke();
        }

        public Vector2 GetMovement()
        {
            return actions.Player.Move.ReadValue<Vector2>();
        }
    }
}