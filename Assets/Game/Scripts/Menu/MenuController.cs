using System;
using System.Collections;
using System.Collections.Generic;
using Game.ScriptableObjects.Puzzles;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject puzzleViewPrefab;

    [SerializeField]
    private GameObject listContainer;
    [Space]
    [SerializeField]
    private PuzzleSO[] puzzleList;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePuzzleViews();
    }

    private void GeneratePuzzleViews()
    {
        for (int i = 0; i < puzzleList.Length; i++)
        {
            GameObject puzzleView = Instantiate(puzzleViewPrefab, listContainer.transform);
            PuzzleView puzzleViewScript = puzzleView.GetComponent<PuzzleView>();
            puzzleViewScript.SetData(puzzleList[i]);
            puzzleViewScript.OnClickButton += OnClickPuzzleButton;
        }
    }

    private void OnClickPuzzleButton(PuzzleSO puzzleData)
    {
        GameManager.Instance.ChangeScene(puzzleData);
    }
}
