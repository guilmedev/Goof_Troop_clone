using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    protected bool _isTransitioning;
    protected Scene _currentScene;
    protected Scene _lastScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransitionToScene(string sceneName, Action OnComplete = null)
    {
        StartCoroutine(TransitionToScene(sceneName, LoadSceneMode.Additive, OnComplete));
    }

    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }

    public void UnLoadLastScene()
    {
        Scene topScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.UnloadSceneAsync(topScene);
    }

    protected IEnumerator TransitionToScene(string newSceneName, LoadSceneMode loadSceneMode, Action OnComplete = null)
    {
        _isTransitioning = true;

        yield return StartCoroutine(GameManager.Instance.UIManager.FadeSceneOut());

        var topScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        yield return SceneManager.UnloadSceneAsync(topScene);

        yield return SceneManager.LoadSceneAsync(newSceneName, loadSceneMode);

        _currentScene = SceneManager.GetSceneByName(newSceneName);

        yield return StartCoroutine(GameManager.Instance.UIManager.FadeSceneIn());

        _isTransitioning = false;

        OnComplete?.Invoke();
    }
}
