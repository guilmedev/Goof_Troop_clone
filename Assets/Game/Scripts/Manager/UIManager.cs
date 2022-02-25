using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public enum FadeType
    {
        Black, Loading, GameOver,
    }

    public CanvasGroup faderCanvasGroup;
    public CanvasGroup loadingCanvasGroup;
    public CanvasGroup gameOverCanvasGroup;

    public float fadeDuration = 1.5f;
    private bool _IsFading;

    protected IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup)
    {
        _IsFading = true;
        canvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = finalAlpha;
        _IsFading = false;
        canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator FadeSceneIn()
    {
        CanvasGroup canvasGroup;

        if (faderCanvasGroup.alpha > 0.1f)
            canvasGroup = faderCanvasGroup;
        else if (gameOverCanvasGroup.alpha > 0.1f)
            canvasGroup = gameOverCanvasGroup;
        else
            canvasGroup = loadingCanvasGroup;

        yield return StartCoroutine(Fade(0f, canvasGroup));

        canvasGroup.gameObject.SetActive(false);
    }

    public IEnumerator FadeSceneOut(FadeType fadeType = FadeType.Black)
    {
        CanvasGroup canvasGroup;
        switch (fadeType)
        {
            case FadeType.Black:
                canvasGroup = faderCanvasGroup;
                break;
            case FadeType.GameOver:
                canvasGroup = gameOverCanvasGroup;
                break;
            default:
                canvasGroup = loadingCanvasGroup;
                break;
        }

        canvasGroup.gameObject.SetActive(true);

        yield return StartCoroutine(Fade(1f, canvasGroup));
    }
}
