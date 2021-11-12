using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text buttonText;
    private GameController gameController;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetSpace);
    }
    
    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    private void SetSpace()
    {
        if (gameController.GetPlayerStep())
        {
            buttonText.text = gameController.GetPlayerSide();
            button.interactable = false;
            gameController.EndTurn(gameController.GetPlayerSide());
        }
    }
}