using System;
using System.Collections;
using System.Collections.Generic;
using Loader;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")] [SerializeField] private Button buttonStartGame;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonExit;


    [Header("Text")] [SerializeField] private TMP_Text scoreText;

    private bool exitDialogWindowShowed;

    private void Awake()
    {
        buttonStartGame?.onClick.AddListener(StartGame);

        buttonSettings?.onClick.AddListener(Settings);
        
        buttonExit?.onClick.AddListener(Exit);
    }

    private void Start()
    {
        UIManager.Instance.ShowMainMenuWindow();
        scoreText.text = $"Your score: {ScoreSystem.Instance.GetCurrentScore()}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !exitDialogWindowShowed)
        {
            ShowExitDialogWindow();
        }
    }

    private void ShowExitDialogWindow()
    {
        exitDialogWindowShowed = true;
        List<UIManager.DialogButton> buttons = new List<UIManager.DialogButton>
        {
            new UIManager.DialogButton
            {
                buttonTitle = "yes",
                callback = CloseGame
            },

            new UIManager.DialogButton
            {
                buttonTitle = "no",
                closeAfterClick = true,
                callback = () => { exitDialogWindowShowed = false; }
            }
        };

        UIManager.Instance.ShowDialogWindow("Exit game", "Are you sure?", buttons);
    }

    private void StartGame()
    {
        SceneLoader.Instance.LoadScene("Game");
    }

    private void Settings()
    {
        UIManager.Instance.ShowSettingsWindow();
    }

    private void Exit()
    {
        ShowExitDialogWindow();
    }

    private void CloseGame()
    {
        Application.Quit();
    }
}