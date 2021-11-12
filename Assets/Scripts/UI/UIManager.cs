using System;
using System.Collections.Generic;
using Loader;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private Button buttonSettings;
        [SerializeField] private GameObject[] windows;

        private void Awake()
        {
            buttonSettings?.onClick.AddListener(ShowSettingsWindow);
            Instance = this;
        }

        private void ChangeWindow(int id)
        {
            /*
         * 0 - MainMenuWindow
         * 1 - SettingsWindow
         */
            if (id >= 0 && id <= windows.Length - 1)
            {
                windows[id].SetActive(true);
                windows[id].transform.SetSiblingIndex(transform.childCount);
            }
        }

        public void HideAllWindows()
        {
            foreach (GameObject window in windows)
            {
                window.SetActive(false);
            }
        }

        public void ShowSettingsWindow()
        {
            ChangeWindow(1);
        }

        public void ShowMainMenuWindow()
        {
            ChangeWindow(0);
        }

        #region dialog window

        [Header("Диалоговое окно")]
        // префаб диалогового окна
        [SerializeField]
        private GameObject dialogWindowPrefab;

        // префаб кнопки для диалогового окна
        [SerializeField] private GameObject dialogButtonLayoutPrefab;


        public class DialogButton
        {
            public string buttonTitle;
            public bool closeAfterClick;
            public Action callback;
        }

        public void ShowDialogWindow(string title, string description,
            List<DialogButton> buttons, bool showCrossClose = false, GameObject buttonLayout = null,
            GameObject mainWindow = null)
        {
            GameObject dialogWindow = Instantiate(mainWindow != null ? mainWindow : dialogWindowPrefab, transform);
            DialogWindowManager dialogWindowManager = dialogWindow.GetComponent<DialogWindowManager>();

            dialogWindowManager.textTitle.text = title;
            dialogWindowManager.textDescription.text = description;

            dialogWindowManager.buttonCrossClose.gameObject.SetActive(showCrossClose);
            if (showCrossClose)
                dialogWindowManager.buttonCrossClose.onClick.AddListener(() => { CloseDialogWindow(dialogWindow); });

            foreach (DialogButton button in buttons)
            {
                Button newButton = Instantiate(buttonLayout != null ? buttonLayout : dialogButtonLayoutPrefab,
                    dialogWindowManager.buttonsParent).GetComponent<Button>();
                if (button.closeAfterClick)
                    newButton.onClick.AddListener(() => {CloseDialogWindow(dialogWindow);});
                newButton.GetComponentInChildren<TMP_Text>().text = button.buttonTitle;
                newButton.onClick.AddListener(() => { button.callback?.Invoke(); });
            }
        }

        public void CloseDialogWindow(GameObject dialogWindow, Action callback = null)
        {
            callback?.Invoke();
            Destroy(dialogWindow);
        }

        #endregion
    }
}