using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Game.ScriptableObjects.Puzzles;
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

    public void ChangeScene(PuzzleSO puzzleData)
    {
        _sceneController.TransitionToScene(puzzleData.SceneName, () =>
        {
            currentPuzzle = FindObjectOfType<PuzzleBehaviour>();
            _uiManager.TogglePuzzleName(true, puzzleData.puzzleTitle);

            _playerController.transform.position = currentPuzzle.GetPlayerIniPosition.transform.position;
            _playerController.ToggleControll(true);

            // _uiManager.ToggleMobileButtons(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);            
            _uiManager.ToggleMobileButtons(true);

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

        _uiManager.ToggleMobileButtons(false);

        yield return StartCoroutine(_uiManager.FadeSceneOut(FadeType.Black, .5f));

        _playerController.transform.position = currentPuzzle.GetPlayerIniPosition.position;

        yield return StartCoroutine(_uiManager.FadeSceneIn(.8f));

        _playerController.ToggleControll(true);

        // _uiManager.ToggleMobileButtons(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);
        _uiManager.ToggleMobileButtons(true);

        _restartGameVisualRoutine = null;
    }

    private void OnPuzzleCompleted()
    {
        _playerController.ToggleControll(false);

        _uiManager.TogglePuzzleName(false, "");

        _uiManager.ToggleMobileButtons(false);


        _uiManager.ShowCenterMessageFade(_uiManager.PUZZZLE_COMPLETED_MESSAGE, 1f, () =>
       {
           _sceneController.TransitionToScene(FIRST_VALID_SCENE_NAME);

           currentPuzzle?.OnPuzzleCompleted?.RemoveAllListeners();
           currentPuzzle = null;
           _restartGameVisualRoutine = null;

       });

    }
}
