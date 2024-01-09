using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public Weapon weapon;
    private HealthbarUI healthbarUI;
    private TextMeshProUGUI lifeText;
    private TextMeshProUGUI bulletText;

    // Player stats
    public float moveSpeed;
    public float maxlife;
    public float life;
    public int maxAmmunition;
    public float fireForce;
    public float attackSpeed;
    public float dornen;
    public float baseDamage;
    public float damage;

    // Item lists
    private List<string> commonItems;
    private List<string> rareItems;
    private List<string> epicItems;
    private List<string> legendaryItems;


    private void Awake()
    {
        InstantiateStats();
        InstantiateLists();
        InstantiateUi();
    }

    private void InstantiateUi()
    {
        healthbarUI = GameObject.FindGameObjectWithTag("Healthbar").GetComponentInChildren<HealthbarUI>();

        lifeText = GameObject.Find("LifeText").GetComponentInChildren<TextMeshProUGUI>();
        bulletText = GameObject.Find("Bullets").GetComponentInChildren<TextMeshProUGUI>();

        healthbarUI.SetMaxHealth(maxlife);
        healthbarUI.SetHealth(life);
        
        GetDamage(0);
        BulletChanges(maxAmmunition);
    }

    private void InstantiateStats()
    {
        // Initialize player stats
        moveSpeed = 2.5f;
        maxlife = 10f + GlobalVariables.healthUpgrade - 1;
        life = maxlife;
        maxAmmunition = 2;
        fireForce = 30f;
        attackSpeed = 1f;
        dornen = 0f;
        baseDamage = 3f + GlobalVariables.damageUpgrade - 1;
        damage = 1f;
    }

    public void BulletChanges(int ammunition)
    {
        bulletText.text = ammunition.ToString() + "/" + maxAmmunition.ToString();
    }

    public void GetDamage(float damage)
    {
        life -= damage;
        healthbarUI.SetHealth(life);
        lifeText.text = life.ToString() + "/" + maxlife.ToString();
    }
    public void GetHealth(float health)
    {
        if (life < maxlife)
        {
            life += damage;
            healthbarUI.SetHealth(life);
            lifeText.text = life.ToString() + "/" + maxlife.ToString();
        }
    }


    private void InstantiateLists()
    {

        // Initialize item lists
        commonItems = new List<string>
        {
            "Piercing 1", "Pistol", "Helm", "Mamas Latschen", "Dornen", "Fußball", "Milch", "quick mag",
            "Kleines Waffenwissen", "wenig Munni", "Dieb"
        };

        rareItems = new List<string>
        {
            "Piercing 2", "Rüstungsschuhe", "Kaktus an die Rüstung geklebt", "Schulausbildung(Amerika)",
            "Gewichte", "Marcels Faulheit", "Waffenwissen"
        };

        epicItems = new List<string>
        {
            "Piercing 3", "Maschine Pistole", "Marksmanrifle", "Glass Cannon", "Rüstung", "Laufschuhe",
            "Robins T-Shirt", "Robins Melder", "Robins IQ", "Cedrics Fettrüstung"
        };

        legendaryItems = new List<string>
        {
            "Alle Waffen im Besitz", "Minigun", "50.Cal", "Kartenbetrug"
        };
        
    }

    public void Upgrades(int numberInList, int rarity)
    {
        

        // Map item names to actions using a dictionary
        Dictionary<string, Action> upgradeActions;

        switch (rarity)
        {
            case 0:
                upgradeActions = new Dictionary<string, Action>
                {
                    { "Pistol", () => damage += 1f },
                    { "Helm", () => life += 5f},
                    { "Mamas Latschen", () => moveSpeed += 0.5f },
                    { "Dornen", () => dornen = 1f },
                    { "Milch", () => damage += 1f },
                    { "quick mag", () => attackSpeed *= 0.9f },
                    { "Kleines Waffenwissen", () => { attackSpeed *= 0.95f; damage += 1f; } },
                    { "Dieb", () => damage += 1f }
                };
                break;
            case 1:
                upgradeActions = new Dictionary<string, Action>
                {
                    { "Rüstungsschuhe", () => {/* Add implementation for rare items */} },
                    { "Waffenwissen", () => {/* Add implementation for rare items */} }
                };
                break;
            case 2:
                upgradeActions = new Dictionary<string, Action>
                {
                    { "Maschine Pistole", () => {/* Add implementation for epic items */} },
                    { "Marksmanrifle", () => {/* Add implementation for epic items */} },
                    { "Glass Cannon", () => {/* Add implementation for epic items */} },
                    { "Cedrics Fettrüstung", () => {/* Add implementation for epic items */} }
                };
                break;
            case 3:
                upgradeActions = new Dictionary<string, Action>
                {
                    { "Minigun", () => {/* Add implementation for legendary items */} },
                    { "50.Cal", () => {/* Add implementation for legendary items */} },
                    { "Kartenbetrug", () => {/* Add implementation for legendary items */} }
                };
                break;
            default:
                upgradeActions = new Dictionary<string, Action>();
                break;
        }

        // Apply upgrades based on rarity and item
        string itemName;

        switch (rarity)
        {
            case 0:
                itemName = commonItems[numberInList];
                break;
            case 1:
                itemName = rareItems[numberInList];
                break;
            case 2:
                itemName = epicItems[numberInList];
                break;
            case 3:
                itemName = legendaryItems[numberInList];
                break;
            default:
                itemName = null;
                break;
        }

        if (upgradeActions.ContainsKey(itemName))
        {
            upgradeActions[itemName]();
        }
    }
    private delegate void Action();
}
