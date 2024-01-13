using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenEinstellungen : MonoBehaviour
{

    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        OpenOrCloseSettings(false);
    }

    public void OpenSetting()
    {
        OpenOrCloseSettings(true);
    }
    
    public void CloseSetting()
    {
        OpenOrCloseSettings(false);
    }

    private void OpenOrCloseSettings(bool x)
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

    public void Quit()
    {
        Application.Quit();
    }

}
