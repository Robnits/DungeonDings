using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OpenEinstellungen : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroupSettings;
    private bool settingsIsOpen;

    [SerializeField] 
    private Slider sliderSFX;
    [SerializeField] 
    private Slider sliderMusic;

    public AudioMixer audioMixer; 

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        InstantiateVolume();        
        CloseSettings();
        CloseMenu();
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
        float volume = sliderMusic.value;
        audioMixer.SetFloat("music", volume);
        PlayerPrefs.SetFloat("music", volume);
    }
    
    public void SetSFXVolume()
    {
        float volume = sliderSFX.value;
        audioMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfx", volume);
    }

    private void LoadMusic()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("music");

        SetMusicVolume();
    }
    
    private void LoadSFX()
    {
        sliderSFX.value = PlayerPrefs.GetFloat("sfx");
        
        SetSFXVolume();
    }
}
