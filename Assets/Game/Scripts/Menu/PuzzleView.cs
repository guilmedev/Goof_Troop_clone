using System;
using System.Collections;
using System.Collections.Generic;
using Game.ScriptableObjects.Puzzles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleView : MonoBehaviour
{

    public Action<PuzzleSO> OnClickButton;
    private PuzzleSO _data;

    [Header("View")]
    [SerializeField]
    private TextMeshProUGUI _puzzleTitle;

    [SerializeField]
    private GameObject dificultyContainer;

    [SerializeField]
    private Button uibutton;

    public void SetData(PuzzleSO data)
    {
        this._data = data;

        _puzzleTitle.text = _data?.puzzleTitle;

        SetDificulty(_data.dificulty);

        uibutton.onClick.AddListener(ClickButton);
    }

    private void ClickButton()
    {
        OnClickButton?.Invoke(_data);
    }

    private void SetDificulty(int dificulty)
    {
        for (int i = 0; i < dificulty; i++)
        {
            dificultyContainer.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
