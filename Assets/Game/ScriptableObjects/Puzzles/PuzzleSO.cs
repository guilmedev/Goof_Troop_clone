using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects.Puzzles
{
    [CreateAssetMenu(fileName = "Puzzle", menuName = "ScriptableObjects/Puzzles/New Puzzle")]
    public class PuzzleSO : ScriptableObject
    {
        [Multiline]
        public string puzzleTitle = "";

        [Range((int)0, (int)5)]
        public int dificulty = 0;

        [HideInInspector]
        public string SceneName; // handled by PuzzleSOEditor
    }
}