using System;
using System.Collections;
using System.Collections.Generic;
using Loader;
using UnityEngine;

public class FirstLoad : MonoBehaviour
{
    private void Start()
    {
        SceneLoader.Instance.LoadScene("MainMenu", waitingContinueButton: false);
    }
}
