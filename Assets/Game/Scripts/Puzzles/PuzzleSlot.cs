using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Puzzles
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PuzzleSlot : MonoBehaviour
    {
        //TODO: call actions for slot changed state
        private bool _filled;
        public bool Filled => _filled;

        [SerializeField]
        private BoxCollider2D _collider;

        private void OnTriggerStay2D(Collider2D other) 
        {
            _filled = other.GetComponent<IKickable>() != null;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var _other = other.GetComponent<IKickable>();
            if (other != null)
            {
                _filled = false;
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
