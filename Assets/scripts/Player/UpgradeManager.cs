using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    private TextMeshProUGUI buttonText1;
    private TextMeshProUGUI buttonText2;
    private TextMeshProUGUI buttonText3;

    public List<string> commonItems;
    public List<string> rareItems;
    public List<string> EpicItems;
    public List<string> LegendaryItems;
    private List<List<string>> lists;
    
    GameObject button1;
    GameObject button2;
    GameObject button3;

    private GameObject canvas;
    private GameObject Player;

    private bool chose = true;

    private int[] tempaugments = new int[3]; // Initialize the array with size 3

    private void Awake()
    {
        commonItems = new List<string>();
        rareItems = new List<string>();
        EpicItems = new List<string>();
        LegendaryItems = new List<string>();
        lists = new List<List<string>> { commonItems, rareItems, EpicItems, LegendaryItems };

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

        rareItems.Add("Piercing 2");
        rareItems.Add("Rüstungsschuhe");
        rareItems.Add("Kaktus an die Rüstung geklebt");
        rareItems.Add("Schulausbildung(Amerika)");
        rareItems.Add("Gewichte");
        rareItems.Add("Marcels Faulheit");
        rareItems.Add("Waffenwissen");

        EpicItems.Add("Piercing 3");
        EpicItems.Add("Maschine Pistole");
        EpicItems.Add("Marksmanrifle");
        EpicItems.Add("Glass Cannon");
        EpicItems.Add("Rüstung");
        EpicItems.Add("Laufschuhe");
        EpicItems.Add("Robins T-Shirt");
        EpicItems.Add("Robins Melder");
        EpicItems.Add("Robins IQ");
        EpicItems.Add("Cedrics Fettrüstung");

        LegendaryItems.Add("Alle Waffen im Besitz");
        LegendaryItems.Add("Minigun");
        LegendaryItems.Add("50.Cal");
        LegendaryItems.Add("Kartenbetrug");
    }
    private void Start()
    {
        // Find the canvas and player in Start
        canvas = GameObject.Find("Canvas");
        Player = GameObject.FindGameObjectWithTag("Player");

        canvas.SetActive(false);
    }

    public void ItemRoll()
    {
        if (chose)
        {

            chose = false;
            canvas.SetActive(true);
            Time.timeScale = 0;

            button1 = GameObject.Find("Button1");
            button2 = GameObject.Find("Button2");
            button3 = GameObject.Find("Button3");

            buttonText1 = button1.GetComponentInChildren<TextMeshProUGUI>();
            buttonText2 = button2.GetComponentInChildren<TextMeshProUGUI>();
            buttonText3 = button3.GetComponentInChildren<TextMeshProUGUI>();

            for (int i = 0; i <= 2; i++)
            {
                int rarity = Random.Range(0, 100);
                if (rarity == 0)
                {
                    GenerateRandomItem(i, LegendaryItems, Color.yellow);
                }
                else if (rarity < 5 && rarity > 0)
                {
                    GenerateRandomItem(i, EpicItems, Color.magenta);
                }
                else if (rarity < 15 && rarity >= 5)
                {
                    GenerateRandomItem(i, rareItems, Color.blue);
                }
                else if (rarity >= 15)
                {
                    GenerateRandomItem(i, commonItems, Color.green);
                }
            }
        }
    }
    private int hilf;

    private void GenerateRandomItem(int i, List<string> itemList, Color buttonColor)
    {

        if (itemList.Count >= 3)
        {
            do
            {
                hilf = Random.Range(0, itemList.Count);
            } while (hilf == tempaugments[0] || hilf == tempaugments[1] || hilf == tempaugments[2]);
        }
        else
        {
            ItemRoll();
        }
        // Use the correct button and color based on the value of i
        if (i == 0)
        {
            buttonText1.text = itemList[hilf];
            button1.GetComponent<Image>().color = buttonColor;
        }
        else if (i == 1)
        {
            buttonText2.text = itemList[hilf];
            button2.GetComponent<Image>().color = buttonColor;
        }
        else if (i == 2)
        {
            buttonText3.text = itemList[hilf];
            button3.GetComponent<Image>().color = buttonColor;
        }

        tempaugments[i] = hilf; // Update tempaugments after selecting the item
    }
    public void ButtonPressed1()
    {
        if (buttonText1 != null)
            Buttonpressed(buttonText1);
        else
            Debug.Log("buttonText1 is null");
    }
    public void ButtonPressed2()
    {
        if (buttonText2 != null)
            Buttonpressed(buttonText2);
        else
            Debug.Log("buttonText2 is null");
    }
    public void ButtonPressed3()
    {
        if (buttonText3 != null)
            Buttonpressed(buttonText3);
        else
            Debug.Log("buttonText3 is null");
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
                    canvas.SetActive(false);
                }
            }
        }
    }

    private void UpgradeAndDeletion(int positionInList, int rarity)
    {
        Player.GetComponent<Player_Stats>().Upgrades(positionInList, rarity);
        if (rarity == 0)
            commonItems.Remove(commonItems[positionInList]);
        else if (rarity == 1)
            rareItems.Remove(rareItems[positionInList]);
        else if (rarity == 2)
            EpicItems.Remove(EpicItems[positionInList]);
        else if (rarity == 3)
            LegendaryItems.Remove(LegendaryItems[positionInList]);

        chose = true;
    }
}
