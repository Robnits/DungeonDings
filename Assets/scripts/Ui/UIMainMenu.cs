using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField] 
    private CanvasGroup CanvasGroupSettings;

    [SerializeField] 
    private Slider sliderSFX;

    [SerializeField] 
    private Slider sliderMusic;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private TMP_Dropdown dropdown;

    [SerializeField]
    private Toggle toggle;

    public bool settingsIsOpen = false;


    Resolution[] resolutions;

    void Awake()
    {
        InstantiateVolume();
        InstantiateResolution();
    }

    private void InstantiateVolume()
    {
        if (PlayerPrefs.HasKey("music"))
            LoadMusic();
        else
            SetMusicVolume();

        if (PlayerPrefs.HasKey("sfx"))
            LoadSFX();
        else
            SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        if (sliderMusic != null)
        {
            float volume = sliderMusic.value;
            audioMixer.SetFloat("music", volume);
            PlayerPrefs.SetFloat("music", volume);
        }
    }

    public void SetSFXVolume()
    {
        if (sliderSFX != null)
        {
            float volume = sliderSFX.value;
            audioMixer.SetFloat("sfx", volume);
            PlayerPrefs.SetFloat("sfx", volume);
        }
    }

    private void LoadMusic()
    {
        if (sliderMusic != null)
            sliderMusic.value = PlayerPrefs.GetFloat("music");

        SetMusicVolume();
    }

    private void LoadSFX()
    {
        if (sliderSFX != null)
            sliderSFX.value = PlayerPrefs.GetFloat("sfx");

        SetSFXVolume();
    }

    private void InstantiateResolution()
    {
        resolutions = Screen.resolutions;

        if (dropdown != null)
            dropdown.ClearOptions();

        List<string> options = new(); 

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

        if (dropdown != null)
        {
            dropdown.AddOptions(options); 
            dropdown.value = currentIndex;
            dropdown.RefreshShownValue();
        }
    }

    public void OpenSettings(int x)
    {
        if (x != 0)
            settingsIsOpen = true;
        else
            settingsIsOpen = false;
        
        switch (x) 
        {
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
        if (x)
            canvasGroup.alpha = 1f;

        
        canvasGroup.interactable = x;
        canvasGroup.enabled = true;
        canvasGroup.blocksRaycasts = x;
        
    }
    
    private void OpenOrCloseSettings(bool x)
    {
        if (x)
            CanvasGroupSettings.alpha = 1f;
        else
            CanvasGroupSettings.alpha = 0f; 
        CanvasGroupSettings.interactable = x;
        CanvasGroupSettings.enabled = true;
        CanvasGroupSettings.blocksRaycasts = x;
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
