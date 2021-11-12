using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BotController : MonoBehaviour
    {
        public void BotStep(TMP_Text[] buttonList, string botSide)
        {
            int index = GetRandomIndexFromButtonList(buttonList);
            buttonList[index].text = botSide;
            buttonList[index].GetComponentInParent<Button>().interactable = false;
        }

        private int GetRandomIndexFromButtonList(TMP_Text[] buttonList)
        {
            int index = Random.Range(0, buttonList.Length);

            if (buttonList[index].text != "")
            {
                return GetRandomIndexFromButtonList(buttonList);
            }
            else
                return index;
        }
    }
}