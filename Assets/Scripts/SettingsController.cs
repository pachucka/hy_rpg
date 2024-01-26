using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider musicSlider, effectsSlider;

    public GameObject soundPanel;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    int currentResIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleEffects()
    {
        AudioManager.instance.ToggleEffects();
    }

    public void changeMusicVolume()
    {
        AudioManager.instance.ChangeMusicVolume(musicSlider.value);
    }

    public void changeEffectsVolume()
    {
        AudioManager.instance.ChangeEffectsVolume(effectsSlider.value);
    }

    public void openSettings()
    {
        if(soundPanel != null && !soundPanel.activeInHierarchy)
        {
            soundPanel.SetActive(true);
        }
    }

    public void closeSettings()
    {
        if (soundPanel != null && soundPanel.activeInHierarchy)
        {
            soundPanel.SetActive(false);
        }
    }

    public void setQuality(int qualityIndex)
    {
        Debug.Log(QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void ToogleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void setResolution(int resIndex)
    {
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, Screen.fullScreen);
    }
}
