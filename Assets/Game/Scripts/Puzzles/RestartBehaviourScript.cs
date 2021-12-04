using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzles
{
    public class RestartBehaviourScript : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";

        private PuzzleBehaviour _puzzleBehaviour;

        [SerializeField]
        private GameObject _playerSpawnPoint;

        private void Awake() 
        {
            _puzzleBehaviour = GetComponentInParent<PuzzleBehaviour>();
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.tag == PLAYER_TAG)
            {
                other.transform.position = _playerSpawnPoint.transform.position;
                _puzzleBehaviour?.RestartPuzzle();
            }
        }
    }
}
