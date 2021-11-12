using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private float volume;
    [SerializeField] private AudioSource audioSource;
    public static MusicController Instance;
    private void Awake()
    {
       Instance = this;
        if (PlayerPrefs.HasKey("Volume"))
            volume = PlayerPrefs.GetFloat("Volume");
        else
        {
            PlayerPrefs.SetFloat("Volume", 100);
            volume = 1;
        }

        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume * 100;
    }

    public void ChangeVolume(float value)
    {
        volume = value / 100;
        audioSource.volume = volume;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
