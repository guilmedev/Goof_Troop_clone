using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public readonly string PUZZZLE_COMPLETED_MESSAGE = "Puzzle Completed!";


    public enum FadeType
    {
        Black, CenterMessage, CornerMessage,
    }

    [SerializeField]
    private TextMeshProUGUI _centerMessageText;
    [SerializeField]
    private TextMeshProUGUI _cornerMessageText;

    [Space]

    public CanvasGroup faderCanvasGroup;
    public CanvasGroup centerMsgCanvasGroup;
    public CanvasGroup cornerMsgCanvasGroup;

    [Space]
    [SerializeField]
    private GameObject _mobileButtons;


    private bool _IsFading;

    public void ShowCenterMessageFade(string message, float fadeDuration = 1f, Action OnComplete = null)
    {
        StartCoroutine(MessageRoutine(message, fadeDuration, () => OnComplete?.Invoke()));
    }

    public void TogglePuzzleName(bool show, string message = "")
    {
        _cornerMessageText.text = message;
        cornerMsgCanvasGroup.alpha = show ? 1 : 0;
        cornerMsgCanvasGroup.gameObject.SetActive(show);
    }

    public IEnumerator MessageRoutine(string message, float fadeDuration = 1f, Action OnComplete = null)
    {
        _centerMessageText.text = message;

        yield return StartCoroutine(FadeSceneOut(FadeType.CenterMessage, 1f));

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(Fade(0f, centerMsgCanvasGroup, fadeDuration));

        centerMsgCanvasGroup.gameObject.SetActive(false);

        OnComplete?.Invoke();
    }

    public void ToggleMobileButtons(bool show)
    {
        _mobileButtons.SetActive(show);
    }



    protected IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup, float fadeDuration = 1f)
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

    public IEnumerator FadeSceneIn(float fadeDuration = 1f, Action OnComplete = null)
    {
        CanvasGroup canvasGroup;

        if (faderCanvasGroup.alpha > 0.1f)
            canvasGroup = faderCanvasGroup;
        else if (cornerMsgCanvasGroup.alpha > 0.1f)
            canvasGroup = cornerMsgCanvasGroup;
        else
            canvasGroup = centerMsgCanvasGroup;

        yield return StartCoroutine(Fade(0f, canvasGroup, fadeDuration));

        canvasGroup.gameObject.SetActive(false);

        OnComplete?.Invoke();
    }

    public IEnumerator FadeSceneOut(FadeType fadeType = FadeType.Black, float fadeDuration = 1f, Action OnComplete = null)
    {
        CanvasGroup canvasGroup;
        switch (fadeType)
        {
            case FadeType.Black:
                canvasGroup = faderCanvasGroup;
                break;
            case FadeType.CornerMessage:
                canvasGroup = cornerMsgCanvasGroup;
                break;
            default:
                canvasGroup = centerMsgCanvasGroup;
                break;
        }

        canvasGroup.gameObject.SetActive(true);

        yield return StartCoroutine(Fade(1f, canvasGroup, fadeDuration));

        OnComplete?.Invoke();
    }
}
