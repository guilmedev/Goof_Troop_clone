using UnityEngine;

namespace Game.Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        //References
        private Rigidbody2D _rigidbody2D;
        private PlayerMovement _playerMovement;
        private AnimatorController _animatorController;
        //Getters
        public float PlayerVelocity => _playerMovement.GetMovement().magnitude;
        public Vector2 PlayerInput => _playerMovement.GetMovement();
        private Vector2 _idleDirection;
        public Vector2 IdleDirection => _idleDirection;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animatorController = GetComponentInChildren<AnimatorController>();
        }
        void Start()
        {

        }

        private void OnEnable()
        {
            _playerMovement.OnPlayerClick += OnPlayerkick;
        }

        void OnDisable()
        {
            _playerMovement.OnPlayerClick -= OnPlayerkick;
        }

        private void OnPlayerkick()
        {
            Debug.Log("Kick in " + _idleDirection);

            _animatorController.KickAnim();

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
    }

}
