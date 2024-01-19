using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class OpenEinstellungen : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    [SerializeField] 
    private CanvasGroup CanvasGroupSettings;
    private bool settingsIsOpen;
    [SerializeField] 
    private CanvasGroup canvasGroupSteuerungUI;

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


    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (PlayerPrefs.HasKey("Tutorialhelp"))
            LoadTutorial();

        InstantiateVolume();
        InstantiateResolution();
        CloseSettings();
        CloseMenu();
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
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }
        if (dropdown != null)
        {
            dropdown.AddOptions(options);
            dropdown.value = currentIndex;
            dropdown.RefreshShownValue();
        }
    }

    public void OpenMenu()
    {
        if (!settingsIsOpen)
            OpenOrCloseMenu(true);
    }
    
    public void CloseMenu()
    {
        OpenOrCloseMenu(false);
    }

    public void OpenSettings()
    {
        settingsIsOpen = true;
        OpenOrCloseSettings(true);
    }
    
    public void CloseSettings()
    {
        settingsIsOpen = false;
        OpenOrCloseSettings(false);
    }

    private void OpenOrCloseMenu(bool x)
    {
        if (canvasGroup != null && !settingsIsOpen)
        {
            if (x)
            {
                Time.timeScale = 0;
                canvasGroup.alpha = 1f;
            }
            else
            {
                Time.timeScale = 1;
                canvasGroup.alpha = 0f;
            }
            canvasGroup.interactable = x;
            canvasGroup.enabled = true;
            canvasGroup.blocksRaycasts = x;
        }
    }
    
    private void OpenOrCloseSettings(bool x)
    {
        if (CanvasGroupSettings != null)
        {
            if (x)
            {
                Time.timeScale = 0;
                CanvasGroupSettings.alpha = 1f;
            }
            else
            {
                CanvasGroupSettings.alpha = 0f;
            }
            CanvasGroupSettings.interactable = x;
            CanvasGroupSettings.enabled = true;
            CanvasGroupSettings.blocksRaycasts = x;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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
        if(sliderMusic != null)
            sliderMusic.value = PlayerPrefs.GetFloat("music");

        SetMusicVolume();
    }
    
    private void LoadSFX()
    {
        if (sliderSFX != null)
            sliderSFX.value = PlayerPrefs.GetFloat("sfx");
        
        SetSFXVolume();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    
    public void ShowSteuerung(bool alpha)
    {
        canvasGroupSteuerungUI.alpha = alpha ? 1 : 0;
        
        PlayerPrefs.SetInt("Tutorialhelp", alpha ? 1 : 0);
    }

    private void LoadTutorial()
    {
        if (toggle != null)
        {
            bool showTutorial = PlayerPrefs.GetInt("Tutorialhelp") == 1;
            canvasGroupSteuerungUI.alpha = showTutorial ? 1 : 0;
            toggle.isOn = showTutorial;
        }
    }
}
