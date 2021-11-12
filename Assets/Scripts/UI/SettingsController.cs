using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private TMP_Text musicVolumeText;

    private void Awake()
    {
        musicVolumeSlider.onValueChanged.AddListener((MusicVolumeChanger));
        musicVolumeText.text = $"{musicVolumeSlider.value} %";
    }

    private void MusicVolumeChanger(float value)
    {
        musicVolumeText.text = $"{value} %";
    }
}