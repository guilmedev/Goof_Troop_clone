using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzles
{
    public class PuzzleBehaviour : MonoBehaviour
    {
        public UnityEvent OnPuzzleRestarted;
        public UnityEvent OnPuzzleCompleted;
        public UnityEvent OnPuzzleLeave;


        private PuzzleSlot[] _slots;
        private RockPuzzle[] _rocks;
        private Vector3[] _rocksInitalPosition;


        [Header("References")]
        [SerializeField]
        private GameObject _puzzleGate;

        [SerializeField]
        private GameObject _playerInitPos;


        public Transform GetPlayerIniPosition => _playerInitPos.transform;
        private GameObject _playerRef;


        private void Awake()
        {
            _slots = GetComponentsInChildren<PuzzleSlot>();
            _rocks = GetComponentsInChildren<RockPuzzle>();
        }

        void Start()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].OnPuzzleSlotChanged.AddListener(OnSlotChangeFilled);
            }
            StoreRockComponentsPosition();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].OnPuzzleSlotChanged.RemoveListener(OnSlotChangeFilled);
            }
        }

        private void StoreRockComponentsPosition()
        {
            _rocksInitalPosition = new Vector3[_rocks.Length];

            for (int i = 0; i < _rocks.Length; i++)
            {
                _rocksInitalPosition[i] = _rocks[i].transform.position;
            }
        }

        private void RestoreRocksPosition()
        {
            for (int i = 0; i < _rocks.Length; i++)
            {
                _rocks[i].transform.SetPositionAndRotation(_rocksInitalPosition[i], Quaternion.identity);
            }
        }

        public void RestartPuzzle()
        {
            RestoreRocksPosition();
            _puzzleGate?.SetActive(true);
            OnPuzzleRestarted?.Invoke();
        }

        private void OnSlotChangeFilled()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (!_slots[i].Filled)
                {
                    return;
                }
            }
            _puzzleGate?.SetActive(false);

            OnPuzzleCompleted?.Invoke();
        }


    }
}
