using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    private TextMeshProUGUI buttonText1;
    private TextMeshProUGUI buttonText2;
    private TextMeshProUGUI buttonText3;

    public List<string> commonItems;
    public List<string> rareItems;
    public List<string> epicItems;
    public List<string> legendaryItems;
    private List<List<string>> lists;
    
    GameObject button1;
    GameObject button2;
    GameObject button3;

    private CanvasGroup canvas;
    private GameObject Player;
    private int hilf;

   


    private bool chose = true;

    private readonly int[] tempaugments = new int[3]; 

    private void Awake()
    {
        commonItems = new List<string>
    {
        "Hartes Geschoss",
        "Helm",
        "Laufschuhe",
        "Super Dash",
        "Grosses Magazin",
        "Energy Drink",
        "Schneller Schuss",
        "Kleines Waffenwissen",
        "Licht und Schatten",
        "Big Shot"
    };

        rareItems = new List<string>
    {
        "Ruestungsschuhe",
        "Schulausbildung(Amerika)",
        "Gewichte",
        "Flower of Speed",
        "Waffenwissen"
    };

        epicItems = new List<string>
    {
        "Maschine Pistole",
        "Marksmanrifle",
        "Bumm Bumm Frucht",
        "Schwere Ruestung"
    };

        legendaryItems = new List<string>
    {
        "Deathsentence",
        "Minigun",
        "Ultra Boots",
        "Legendary Dash",
        "Glass Cannon"
    };

        lists = new List<List<string>> { commonItems, rareItems, epicItems, legendaryItems };
    }
    private void Start()
    {
        if (!GlobalVariables.isInBossFight)
        {
            canvas = GameObject.Find("AugmentAuswahl").GetComponent<CanvasGroup>();
            ShowItemAuswahl(false);
        }
        
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ItemRoll()
    {
        if (chose)
        {

            chose = false;
            ShowItemAuswahl(true);
            Time.timeScale = 0;

            button1 = GameObject.Find("Button1");
            button2 = GameObject.Find("Button2");
            button3 = GameObject.Find("Button3");

            buttonText1 = button1.GetComponentInChildren<TextMeshProUGUI>();
            buttonText2 = button2.GetComponentInChildren<TextMeshProUGUI>();
            buttonText3 = button3.GetComponentInChildren<TextMeshProUGUI>();
            if(HasItems())
            {
                for (int i = 0; i <= 2; i++)
                {
                    int rarity = Random.Range(0, 100);
                    if (rarity == 0)
                    {
                        GenerateRandomItem(i, legendaryItems, Color.yellow);
                    }
                    else if (rarity < 6 && rarity > 0)
                    {
                        GenerateRandomItem(i, epicItems, Color.magenta);
                    }
                    else if (rarity < 20 && rarity >= 6)
                    {
                        GenerateRandomItem(i, rareItems, Color.blue);
                    }
                    else if (rarity >= 20)      
                    {
                        GenerateRandomItem(i, commonItems, Color.green);
                    }else{
                        
                    }
                }
            }else
            {
                Debug.LogError("Keine Items verfügbar!");
                chose = true; 
            }
        }
    }
    private bool HasItems()
    {   
        foreach (var list in lists)
    {
        if (list.Count > 0)
            return true;
    }
    return false;
    }



    private void GenerateRandomItem(int i, List<string> itemList, Color buttonColor)
{
    // Shuffle the itemList using Fisher-Yates shuffle algorithm
    for (int index = 0; index < itemList.Count - 1; index++)
    {
        int randomIndex = Random.Range(index, itemList.Count);
        string temp = itemList[index];
        itemList[index] = itemList[randomIndex];
        itemList[randomIndex] = temp;
    }

    // Select the first three items from the shuffled list
    if (i == 0 && itemList.Count > 0)
    {
        buttonText1.text = itemList[0];
        button1.GetComponent<Image>().color = buttonColor;
        tempaugments[0] = itemList.IndexOf(buttonText1.text); // Store the index of the selected item
    }
    else if (i == 1 && itemList.Count > 1)
    {
        buttonText2.text = itemList[1];
        button2.GetComponent<Image>().color = buttonColor;
        tempaugments[1] = itemList.IndexOf(buttonText2.text); // Store the index of the selected item
    }
    else if (i == 2 && itemList.Count > 2)
    {
        buttonText3.text = itemList[2];
        button3.GetComponent<Image>().color = buttonColor;
        tempaugments[2] = itemList.IndexOf(buttonText3.text); // Store the index of the selected item
    }
}
    public void ButtonPressed1()
    {
        Buttonpressed(buttonText1);
    }
    public void ButtonPressed2()
    {
        Buttonpressed(buttonText2);
    }
    public void ButtonPressed3()
    {
        Buttonpressed(buttonText3);
    }

    public void Buttonpressed(TextMeshProUGUI button)
    {
        for (int j = 0; j < lists.Count; j++)
        {
            for (int i = 0; i < lists[j].Count; i++)
            {
                if (button.text == lists[j][i])
                {
                    UpgradeAndDeletion(i, j);
                    Time.timeScale = 1;
                    ShowItemAuswahl(false);
                }
            }
        }
    }

    private void UpgradeAndDeletion(int positionInList, int rarity)
    {
        Player.GetComponent<player_Stats>().Upgrades(positionInList, rarity);
        if (rarity == 0)
            commonItems.Remove(commonItems[positionInList]);
        else if (rarity == 1)
            rareItems.Remove(rareItems[positionInList]);
        else if (rarity == 2)
            epicItems.Remove(epicItems[positionInList]);
        else if (rarity == 3)
            legendaryItems.Remove(legendaryItems[positionInList]);

        chose = true;
    }

    public void ShowItemAuswahl(bool hilf){
        if(hilf){
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
            canvas.interactable = true;
        }else{
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
        }
    }
}
