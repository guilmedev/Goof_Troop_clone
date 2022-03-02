using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Puzzles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UIManager;

public class GameManager : Singleton<GameManager>
{
    [Header("Parameters")]
    public const string FIRST_VALID_SCENE_NAME = "Menu";

    [Header("References")]
    [SerializeField]
    private GameObject playerPrefab;
    private PlayerController _playerController;
    private SceneController _sceneController;
    private UIManager _uiManager;

    public UIManager UIManager => _uiManager;
    private PuzzleBehaviour currentPuzzle = null;
    public PlayerController PlayerController => _playerController;

    private Coroutine _restartGameVisualRoutine = null;


    //TODO Player ref (Instatiate once) - Toggle visibility and controlls

    protected override void Awake()
    {
        _sceneController = GetComponent<SceneController>();
        _uiManager = GetComponentInChildren<UIManager>();
    }

    protected override void Start()
    {
        _sceneController.LoadScene(FIRST_VALID_SCENE_NAME, LoadSceneMode.Additive);

        //Cache Player
        _playerController = Instantiate(playerPrefab).GetComponent<PlayerController>();
        _playerController.ToggleControll(false);
    }

    public void ChangeScene(string sceneName)
    {
        _sceneController.TransitionToScene(sceneName, () =>
        {
            currentPuzzle = FindObjectOfType<PuzzleBehaviour>();

            _playerController.transform.position = currentPuzzle.GetPlayerIniPosition.transform.position;
            _playerController.ToggleControll(true);

            //Puzzle events
            currentPuzzle?.OnPuzzleCompleted?.AddListener(OnPuzzleCompleted);
            currentPuzzle?.OnPuzzleRestarted?.AddListener(OnPuzzleRestarted);
        });
    }

    private void OnPuzzleRestarted()
    {
        if (_restartGameVisualRoutine == null)
        {
            _restartGameVisualRoutine = StartCoroutine(RestartGameVisualRoutine());
        }
    }

    protected IEnumerator RestartGameVisualRoutine()
    {
        _playerController.ToggleControll(false);

        yield return StartCoroutine(_uiManager.FadeSceneOut(FadeType.Black, .5f));

        _playerController.transform.position = currentPuzzle.GetPlayerIniPosition.position;

        yield return StartCoroutine(_uiManager.FadeSceneIn(.5f));

        _playerController.ToggleControll(true);

        _restartGameVisualRoutine = null;
    }

    private void OnPuzzleCompleted()
    {
        _playerController.ToggleControll(false);

        _sceneController.TransitionToScene("Menu");

        currentPuzzle?.OnPuzzleCompleted?.RemoveAllListeners();
        currentPuzzle = null;
        _restartGameVisualRoutine = null;
    }
}
