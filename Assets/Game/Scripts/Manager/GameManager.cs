using System;
using System.Collections;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Parameters")]
    public const string FIRST_VALID_SCENE_NAME = "Menu";

    [Header("References")]
    [SerializeField]
    private GameObject playerPrefab;

    private SceneController _sceneController;
    private UIManager _uiManager;
    public UIManager UIManager => _uiManager;
    private PuzzleBehaviour currentPuzzle = null;

    //TODO Player ref (Instatiate once) - Toggle visibility and controlls

    protected override void Awake()
    {
        _sceneController = GetComponent<SceneController>();
        _uiManager = GetComponentInChildren<UIManager>();
    }

    protected override void Start()
    {
        _sceneController.LoadScene(FIRST_VALID_SCENE_NAME, LoadSceneMode.Additive);
    }

    public void ChangeScene(string sceneName)
    {
        _sceneController.TransitionToScene(sceneName, () =>
        {
            currentPuzzle = FindObjectOfType<PuzzleBehaviour>();
            currentPuzzle.SetUpPlayerPosition(playerPrefab);
            currentPuzzle?.OnPuzzleCompleted?.AddListener(OnPuzzleCompleted);
        });
    }

    private void OnPuzzleCompleted()
    {
        _sceneController.TransitionToScene("Menu");

        //TODO 'Unload player'
        currentPuzzle?.OnPuzzleCompleted?.RemoveAllListeners();
        currentPuzzle = null;
    }
}
