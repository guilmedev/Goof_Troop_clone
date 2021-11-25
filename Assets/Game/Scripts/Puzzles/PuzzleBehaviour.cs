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

        private void Awake()
        {
            _slots = GetComponentsInChildren<PuzzleSlot>();
            _rocks = GetComponentsInChildren<RockPuzzle>();                    
        }

        

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
