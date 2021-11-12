using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    [SerializeField] private Slider volume;

    private void Start()
    {
        volume.onValueChanged.AddListener((val) =>
        {
            MusicController.Instance.ChangeVolume(val);    
        });
        volume.value = MusicController.Instance.GetVolume();
    }
}
