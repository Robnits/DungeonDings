using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroup canvasGroupSettings;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private bool isOn;

    public bool settingsIsOpen = false;

    private Resolution[] resolutions;

    private void Awake()
    {
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        LoadVolumeSettings();
        LoadResolutionSettings();
        LoadFullscreenSettings();
    }

    private void LoadVolumeSettings()
    {
        if (sliderMusic != null)
            sliderMusic.value = PlayerPrefs.GetFloat("music", 1f);
        if (sliderSFX != null)
            sliderSFX.value = PlayerPrefs.GetFloat("sfx", 1f);
    }

    private void LoadResolutionSettings()
    {
        resolutions = Screen.resolutions;
        if (resolutionDropdown != null)
        {
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                if (!options.Contains(option))
                {
                    options.Add(option);
                    if (resolutions[i].width == Screen.currentResolution.width &&
                        resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentIndex = options.Count - 1;
                    }
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }

    private void LoadFullscreenSettings()
    {
        if (fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("music", volume);
        PlayerPrefs.SetFloat("music", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfx", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OpenSettings(int x)
    {
        if (x != 0)
        {
            settingsIsOpen = true;
            Time.timeScale = 0;
        }
        else
            settingsIsOpen = false;
        
        switch (x) 
        {
            case 0:
                Time.timeScale = 1;
                OpenOrCloseMenu(false);
                OpenOrCloseSettings(false);
                break;
            case 1:
                OpenOrCloseSettings(false);
                OpenOrCloseMenu(true);
                break;
            case 2:
                OpenOrCloseMenu(false);
                OpenOrCloseSettings(true);
                break;
        }
    }

    private void OpenOrCloseMenu(bool x)
    {
        if (canvasGroup != null)
        {
            
        
        if (x)
            canvasGroup.alpha = 1f;
        else
            canvasGroup.alpha = 0f;
        
        canvasGroup.interactable = x;
        canvasGroup.enabled = true;
        canvasGroup.blocksRaycasts = x;
        }
    }
    
    private void OpenOrCloseSettings(bool x)
    {
        if(canvasGroup != null)
        {
            if (x)
                canvasGroupSettings.alpha = 1f;
            else
                canvasGroupSettings.alpha = 0f;
    
            canvasGroupSettings.interactable = x;
            canvasGroupSettings.enabled = true;
            canvasGroupSettings.blocksRaycasts = x;
        }
    }


    public void Quit()
    {
        Application.Quit();
    }
}
