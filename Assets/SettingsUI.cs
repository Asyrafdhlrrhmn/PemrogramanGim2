using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Slider musicSlider;

    public Slider sfxSlider;

    void Start()
    {
        // set slider value
        musicSlider.value =
            AudioManager.instance
            .GetMusicVolume();

        sfxSlider.value =
            AudioManager.instance
            .GetSFXVolume();

        // listener
        musicSlider.onValueChanged
            .AddListener(SetMusicVolume);

        sfxSlider.onValueChanged
            .AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance
            .SetMusicVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.instance
            .SetSFXVolume(value);
    }
}