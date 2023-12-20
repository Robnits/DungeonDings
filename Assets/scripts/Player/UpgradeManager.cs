using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    TextMeshProUGUI buttonText1;
    TextMeshProUGUI buttonText2;
    TextMeshProUGUI buttonText3;
    private List<string> commonItems = new List<string>();
    private List<string> rareItems = new List<string>();
    private List<string> EpicItems = new List<string>();
    private List<string> LegendaryItems = new List<string>();

    public GameObject canvas;
    private GameObject Player;
    public GameObject chest;

    private void Awake()
    {
        buttonText1 = GameObject.Find("Button1").GetComponentInChildren<TextMeshProUGUI>();
        buttonText2 = GameObject.Find("Button2").GetComponentInChildren<TextMeshProUGUI>();
        buttonText3 = GameObject.Find("Button3").GetComponentInChildren<TextMeshProUGUI>();


        commonItems.Clear();
        commonItems.Add("Piercing 1");
        commonItems.Add("Pistol");
        commonItems.Add("Helm");
        commonItems.Add("Mamas Latschen");
        commonItems.Add("Dornen");
        commonItems.Add("Fußball");
        commonItems.Add("Milch");
        commonItems.Add("quick mag");
        commonItems.Add("Kleines Waffenwissen");
        commonItems.Add("wenig Munni");
        commonItems.Add("Dieb");

        rareItems.Clear();
        rareItems.Add("Piercing 2");
        rareItems.Add("Rüstungsschuhe");
        rareItems.Add("Kaktus an die Rüstung geklebt");
        rareItems.Add("Schulausbildung(Amerika)");
        rareItems.Add("Gewichte");
        rareItems.Add("Marcels faulheit");
        rareItems.Add("Waffenwissen");
        rareItems.Add("Dual wield maybe");

        EpicItems.Clear();
        EpicItems.Add("Piercing 3");
        EpicItems.Add("Maschine Pistole");
        EpicItems.Add("Marksmanrifle");
        EpicItems.Add("Glass Cannon");
        EpicItems.Add("Rüstung");
        EpicItems.Add("Laufschuhe");
        EpicItems.Add("Robins T-Shirt");
        EpicItems.Add("Robins Melder");
        EpicItems.Add("Robins iq");
        EpicItems.Add("Cedrics Fettrüstung");

        LegendaryItems.Clear();
        LegendaryItems.Add("Alle Waffen im besitz");
        LegendaryItems.Add("Minigun");
        LegendaryItems.Add("50.Cal");
        LegendaryItems.Add("Kartenbetrug");
    }
    private void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ButtonPressed1()
    {
       
        for (int i = 0; i < commonItems.Count; i++)
        {
            if (buttonText1.text == commonItems[i])
            {
                Upgrade(i, 0);
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
                Upgrade(i, 0);
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
                Upgrade(i,0);
                Time.timeScale = 1;
                canvas.SetActive(false);
            }
        }
    }

    private void Upgrade(int positionInList, int rarity)
    {
        Player.GetComponent<Player_behjaviour>().Upgrades(positionInList);
        chest.GetComponent<Chest>().removeTheChosen(positionInList, rarity);
    }
}
