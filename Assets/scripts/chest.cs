using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    public GameObject canvas;
    private List<string> commonItems = new List<string>();
    private List<string> rareItems = new List<string>();
    private List<string> EpicItems = new List<string>();
    private List<string> LegendaryItems = new List<string>();

    private int[] tempaugments = new int[3]; // Initialize the array with size 3

    TextMeshProUGUI buttonText1;
    TextMeshProUGUI buttonText2;
    TextMeshProUGUI buttonText3;

    GameObject button1;
    GameObject button2;
    GameObject button3;

    void Start()
    {
        canvas.SetActive(false);
        ResetList();
    }

    private void GenerateRandomItem(int i, List<string> itemList, Color buttonColor)
    {
        int hilf;
        do
        {
            hilf = Random.Range(0, itemList.Count);
        } while (hilf == tempaugments[0] || hilf == tempaugments[1] || hilf == tempaugments[2]);

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
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
                    print(rarity);

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
    }

    private void ResetList()
    {
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
    public void removeTheChosen(int i, int rarity)
    {
        if (rarity == 0 && i < commonItems.Count)
            commonItems.RemoveAt(i);
        else if (rarity == 1 && i < rareItems.Count)
            rareItems.RemoveAt(i);
        else if (rarity == 2 && i < EpicItems.Count)
            EpicItems.RemoveAt(i);
        else if (rarity == 3 && i < LegendaryItems.Count)
            LegendaryItems.RemoveAt(i);
    }
}

