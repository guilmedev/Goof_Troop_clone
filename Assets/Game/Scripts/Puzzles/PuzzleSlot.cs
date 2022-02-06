using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzles
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PuzzleSlot : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";

        [HideInInspector]
        public UnityEvent OnPuzzleSlotChanged;

        private bool _filled;
        public bool Filled => _filled;

        [SerializeField]
        private BoxCollider2D _collider;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(PLAYER_TAG))
            return;

            var _other = other.GetComponent<IKickable>();
            if (other != null)
            {
                if (_filled) return;

                _filled = true;
                OnPuzzleSlotChanged?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var _other = other.GetComponent<IKickable>();
            if (other != null)
            {
                if (!_filled) return;
                
                _filled = false;

                OnPuzzleSlotChanged?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            if (_collider != null)
            {
                Gizmos.color = _filled ? Color.green : Color.red;

                Gizmos.DrawWireCube(this.transform.position, new Vector3(_collider.size.x, _collider.size.y, 1f));
            }
        }
    }

}
