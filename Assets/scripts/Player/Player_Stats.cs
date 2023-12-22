using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public ScriptableObject upgradeSO;
    
    public float movespeed;

    public float life;

    public int maxAmmunition;
    public float fireforce;
    public float attackSpeed;

    public float dornen;

    public float baseDamage;
    public float damage;

    private bool instantiateList = false;
    private List<string> commonItems;
    private List<string> rareItems;
    private List<string> EpicItems;
    private List<string> LegendaryItems;

    private void Awake()
    {
        movespeed = 2.5f;
        life = 10;

        maxAmmunition = 2;
        fireforce = 30f;
        attackSpeed = 1f;

        dornen = 0f;

        baseDamage = 3f;
        damage = 1f;
    }

    public void Upgrades(int NumberInList, int rarity)
    {
        if (!instantiateList)
        {
            instantiateList = true;
            commonItems = new List<string>();
            rareItems = new List<string>();
            EpicItems = new List<string>();
            LegendaryItems = new List<string>();

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
        switch (rarity)
        {
            case 0:
                switch (commonItems[NumberInList])
                {
                    case "Piercing 1":
                        break;
                    case "Pistol":
                        damage += 1f;
                        break;
                    case "Helm":
                        life += 5f;
                        break;
                    case "Mamas Latschen":
                        movespeed += 0.5f;
                        break;
                    case "Dornen":
                        dornen = 1f;
                        break;
                    case "Fußball":
                        break;
                    case "Milch":
                        damage += 1f;
                        break;
                    case "quick mag":
                        attackSpeed *= 0.9f;
                        break;
                    case "Kleines Waffenwissen":
                        attackSpeed *= 0.95f;
                        damage += 1f;
                        break;
                    case "wenig Munni":
                        break;
                    case "Dieb":
                        damage += 1f;
                        break;
                }
                break;
            case 1:
                switch (rareItems[NumberInList])
                {
                    case "Piercing 2":
                        break;
                    case "Rüstungsschuhe":
                        break;
                    case "Kaktus an die Rüstung geklebt":
                        break;
                    case "Schulausbildung(Amerika)":
                        break;
                    case "Gewichte":
                        break;
                    case "Marcels Faulheit":
                        break;
                    case "Waffenwissen":
                        break;
                }
                break;
            case 2:
                switch (EpicItems[NumberInList])
                {
                    case "Piercing 3":
                        break;
                    case "Maschine Pistole":
                        break;
                    case "Marksmanrifle":
                        break;
                    case "Glass Cannon":
                        break;
                    case "Rüstung":
                        break;
                    case "Laufschuhe":
                        break;
                    case "Robins T-Shirt":
                        break;
                    case "Robins Melder":
                        break;
                    case "Robins IQ":
                        break;
                    case "Cedrics Fettrüstung":
                        break;
                }
                break;
            case 3:
                switch (LegendaryItems[NumberInList])
                {
                    case "Alle Waffen im Besitz":
                        break;
                    case "Minigun":
                        break;
                    case "50.Cal":
                        break;
                    case "Kartenbetrug":
                        break;
                }
                break;
        }

    }
}
