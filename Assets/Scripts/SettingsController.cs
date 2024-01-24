using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider musicSlider, effectsSlider;

    public GameObject soundPanel;

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
}
