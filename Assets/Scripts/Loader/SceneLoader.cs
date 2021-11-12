using System;
using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Loader
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;

        [SerializeField] private TMP_Text loadingText;
        [SerializeField] private Slider loadingSlider;
        [SerializeField] private GameObject loadingWindow;

        // доп переменная для стопинга загрузки
        private bool waitingLoad;

        private void Awake()
        {
            Instance = this;
            
        }


        public void LoadScene(string sceneName, string loadingTitle = "Loading progress: ",
            string finishTitle = "Press left mouse button to continue", bool waitingContinueButton = true,
            KeyCode continueButton = KeyCode.Mouse0)
        {
            UIManager.Instance.HideAllWindows();
            loadingWindow.transform.SetSiblingIndex(UIManager.Instance.gameObject.transform.childCount);
            loadingWindow.SetActive(true);
            StartCoroutine(AsyncLoadScene(sceneName, loadingTitle, finishTitle, waitingContinueButton, continueButton));
        }

        private IEnumerator AsyncLoadScene(string sceneName, string loadingTitle, string finishTitle,
            bool waitingContinueButton,
            KeyCode continueButton)
        {
            yield return null;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                loadingText.text = loadingTitle + (asyncOperation.progress * 100) + "%";
                loadingSlider.value = asyncOperation.progress * 100;
                
                if (asyncOperation.progress >= 0.9f)
                {
                    if (!waitingLoad)
                    {
                        loadingText.text = loadingTitle + (asyncOperation.progress * 100) + "%";
                        loadingSlider.value = asyncOperation.progress * 100;
                        waitingLoad = true;
                    }

                    loadingSlider.value = 100;
                    loadingText.text = finishTitle;
                    if (waitingContinueButton)
                        asyncOperation.allowSceneActivation = Input.GetKeyDown(continueButton);
                    else
                        asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}