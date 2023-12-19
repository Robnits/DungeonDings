using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeManager : MonoBehaviour
{
    TextMeshProUGUI buttonText1;
    TextMeshProUGUI buttonText2;
    TextMeshProUGUI buttonText3;
    private List<string> commonItems = new List<string>();
    public GameObject canvas;

    private void Awake()
    {
        buttonText1 = GameObject.Find("Button1").GetComponentInChildren<TextMeshProUGUI>();
        buttonText2 = GameObject.Find("Button2").GetComponentInChildren<TextMeshProUGUI>();
        buttonText3 = GameObject.Find("Button3").GetComponentInChildren<TextMeshProUGUI>();

        commonItems.Add("Minigun");
        commonItems.Add("Maschine Pistole");
        commonItems.Add("50.Cal");
        commonItems.Add("Marksmanrifle");
        commonItems.Add("Piercing 1");
        commonItems.Add("Piercing 2");
        commonItems.Add("Glass Cannon");
        commonItems.Add("Mamas Latschen");
        commonItems.Add("Dornen");
        commonItems.Add("Milch");
        commonItems.Add("Gewichte");


    }

    public void ButtonPressed1()
    {

        for (int i = 0; i < commonItems.Count; i++)
        {
            if(buttonText1.text == commonItems[i])
            {
                Upgrade(i);
                Time.timeScale = 1;
                canvas.SetActive(false);
            }
        }
    }
    public void ButtonPressed2()
    {

        for (int i = 0; i < commonItems.Count; i++)
        {
            if (buttonText2.text == commonItems[i])
            {
                Upgrade(i);
                Time.timeScale = 1;
                canvas.SetActive(false);
            }
        }
    }
    public void ButtonPressed3()
    {

        for (int i = 0; i < commonItems.Count; i++)
        {
            if (buttonText3.text == commonItems[i])
            {
                Upgrade(i);
                Time.timeScale = 1;
                canvas.SetActive(false);
            }
        }
    }

    private void Upgrade(int positionInList)
    {
        print(commonItems[positionInList]);
    }
}
