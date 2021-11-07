using UnityEngine;

namespace Game.Player
{

    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        #region Constants
        private const string VERTICAL_IDLE_STRING = "VerticalIdle";
        private const string HORIZONTAL_IDLE_STRING = "HorizontalIdle";
        private const string MAGNITUDE_STRING = "InputMagnitude";
        private const string VERTICAL_INPUT_STRING = "VerticalInput";
        private const string HORIZONTAL_IPNUT_STRING = "HorizontalInput";
        private const string KICK_TRIGGER_STRING = "Kick";
        #endregion

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
            _animator.SetFloat(VERTICAL_IDLE_STRING, _playerController.IdleDirection.y);
            _animator.SetFloat(HORIZONTAL_IDLE_STRING, _playerController.IdleDirection.x);


            _animator.SetFloat(MAGNITUDE_STRING, _playerController.PlayerVelocity);
            _animator.SetFloat(VERTICAL_INPUT_STRING, _playerController.PlayerInput.y);
            _animator.SetFloat(HORIZONTAL_IPNUT_STRING, _playerController.PlayerInput.x);

        }

        public void KickAnim()
        {
            _animator.SetTrigger(KICK_TRIGGER_STRING);
        }
    }
}