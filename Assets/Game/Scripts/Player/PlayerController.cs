using System;
using Interfaces;
using UnityEngine;

namespace Game.Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private KickBehaviour _kickBehaviour;
        private Interactor _interactor;



        //References
        [SerializeField]
        private GameObject _playerArtGO;
        private Rigidbody2D _rigidbody2D;
        private AnimatorController _animatorController;
        //Getters
        public float PlayerVelocity => _playerMovement.GetMovement().magnitude;
        public Vector2 PlayerInput => _playerMovement.GetMovement();
        private Vector2 _idleDirection;
        public Vector2 IdleDirection => _idleDirection;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _kickBehaviour = GetComponent<KickBehaviour>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _interactor = GetComponent<Interactor>();

            _animatorController = GetComponentInChildren<AnimatorController>();
        }
        void Start()
        {

        }

        private void OnEnable()
        {
            _playerMovement.OnPlayerClick += OnPlayerkick;
            _kickBehaviour.OnKickSuccess += OnKickSuccess;
            _kickBehaviour.OnKickFail += OnKickFail;
        }


        void OnDisable()
        {
            _playerMovement.OnPlayerClick -= OnPlayerkick;
            _kickBehaviour.OnKickSuccess -= OnKickSuccess;
            _kickBehaviour.OnKickFail -= OnKickFail;
        }

        private void FixedUpdate()
        {
            //TODO: verify glith when stops
            if (_playerMovement.GetMovement().x != 0 || _playerMovement.GetMovement().y != 0)
            {
                _idleDirection = _playerMovement.GetMovement();
            }
            _rigidbody2D.velocity = _playerMovement.GetMovement() * _playerMovement.Speed;
        }

        public void ToggleControll(bool enable)
        {
            _playerMovement.enabled = enable;
            _kickBehaviour.enabled = enable;
            _interactor.enabled = enable;
        }

        public void ToggleVisibility(bool enable)
        {
            _playerArtGO.SetActive(enable);
        }

        public void ToggleControllAndVisibility(bool enable)
        {
            _playerMovement.enabled = enable;
            _kickBehaviour.enabled = enable;
            _interactor.enabled = enable;
            _playerArtGO.SetActive(enable);
        }

        #region CallBacks
        private void OnPlayerkick()
        {
            Vector2 position = ((Vector2)this.transform.position - new Vector2(0, .5f));

            GameObject interactable;

            if (_interactor.Interact(position, _idleDirection, out interactable))
            {
                _kickBehaviour.DoKick(_idleDirection, interactable);
            }
        }
        private void OnKickSuccess()
        {
            _animatorController.KickAnim();
        }
        private void OnKickFail()
        {
            // Debug.Log("Ouch, kick Fail");
        }

        #endregion
    }

}
