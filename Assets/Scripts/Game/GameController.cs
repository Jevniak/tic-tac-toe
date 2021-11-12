using System;
using System.Collections;
using System.Collections.Generic;
using Loader;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof(BotController))]
    public class GameController : MonoBehaviour
    {
        public TMP_Text[] buttonList;
        private string playerSide = "X";
        private string botSide = "0";
        private bool gameOver;

        private bool playerStep;

        private int lastScore;

        [SerializeField] private TMP_Text scoreText;

        private BotController botController;


        void Awake()
        {
            botController = GetComponent<BotController>();
            byte randomSide = Convert.ToByte(Random.Range(0, 2));
            if (randomSide == 0)
            {
                playerStep = true;
                playerSide = "X";
                botSide = "0";
            }
            else
            {
                playerStep = false;
                playerSide = "0";
                botSide = "X";
                botController.BotStep(buttonList, botSide);
                EndTurn(botSide);
            }

            SetGameControllerReferenceOnButtons();
        }

        private void SetGameControllerReferenceOnButtons()
        {
            foreach (TMP_Text button in buttonList)
            {
                button.GetComponentInParent<GridSpace>().SetGameControllerReference(this);
            }
        }

        public string GetPlayerSide()
        {
            return playerSide;
        }

        public bool GetPlayerStep()
        {
            return playerStep;
        }

        public void EndTurn(string side)
        {
            if (buttonList[0].text == side && buttonList[1].text == side && buttonList[2].text == side ||
                buttonList[3].text == side && buttonList[4].text == side && buttonList[5].text == side ||
                buttonList[6].text == side && buttonList[7].text == side && buttonList[8].text == side ||
                buttonList[0].text == side && buttonList[3].text == side && buttonList[6].text == side ||
                buttonList[1].text == side && buttonList[4].text == side && buttonList[7].text == side ||
                buttonList[2].text == side && buttonList[5].text == side && buttonList[8].text == side ||
                buttonList[0].text == side && buttonList[4].text == side && buttonList[8].text == side ||
                buttonList[2].text == side && buttonList[4].text == side && buttonList[6].text == side)
            {
                GameOver(side);
            }


            playerStep = !playerStep;

            if (!playerStep && !gameOver)
            {
                botController.BotStep(buttonList, botSide);
                EndTurn(botSide);
            }
        }


        private void GameOver(string side)
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = $"Your score: {ScoreSystem.Instance.GetCurrentScore()}";
            lastScore = ScoreSystem.Instance.GetCurrentScore();
            if (side == playerSide)
            {
                ScoreSystem.Instance.IncrementScore(100);
                ShowDialogEndGame("You Won ;)");
            }
            else
            {
                ScoreSystem.Instance.DecrementScore(100);
                ShowDialogEndGame("You Lose :(");
            }

            gameOver = true;
            foreach (TMP_Text button in buttonList)
            {
                button.GetComponentInParent<Button>().interactable = false;
            }
            scoreText.gameObject.SetActive(true);
            StartCoroutine(ChangeSmoothText());
        }

        private IEnumerator ChangeSmoothText()
        {
            yield return new WaitForSeconds(1f);
            while (lastScore != ScoreSystem.Instance.GetCurrentScore())
            {
                if (lastScore < ScoreSystem.Instance.GetCurrentScore())
                    lastScore++;
                else
                    lastScore--;
                scoreText.text = $"Your score: {lastScore}";
                yield return new WaitForSeconds(0.01f);
            }
        }

        private void ShowDialogEndGame(string title)
        {
            List<UIManager.DialogButton> buttons = new List<UIManager.DialogButton>
            {
                new UIManager.DialogButton
                {
                    buttonTitle = "yes",
                    callback = () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
                },

                new UIManager.DialogButton
                {
                    buttonTitle = "no",
                    callback = () => { SceneLoader.Instance.LoadScene("MainMenu", waitingContinueButton:false); }
                }
            };

            UIManager.Instance.ShowDialogWindow(title, "Play again?", buttons);
        }
    }
}