using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzles
{
    public class PuzzleBehaviour : MonoBehaviour
    {
        //TODO: store inital position        
        private PuzzleSlot[] _slots;
        private RockPuzzle[] _rocks;
        private Vector3[] _rocksInitalPosition;


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
            //TODO:
            Debug.Log("Puzlle Completed !");
        }


    }
}
