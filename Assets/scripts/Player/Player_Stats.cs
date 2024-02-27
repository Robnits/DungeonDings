using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    private HealthbarUI healthbarUI;
    private TextMeshProUGUI lifeText;
    [SerializeField]
    private GameObject[] bulletUI;


    // Player stats
    public float maxMoveSpeed;
    public float moveSpeed;

    public float maxlife;
    public float life;
    public int maxAmmunition;
    public int ammunition;
    public float fireForce;
    public float attackSpeed;
    public float dornen;
    public float baseDamage;
    public float damage;
    public float dashmaxCooldown;

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
        healthbarUI.SetHealth(life, maxlife);

        bulletUI = GameObject.FindGameObjectsWithTag("BulletUI");

        foreach (var item in bulletUI)
        {
            item.SetActive(false);
        }
        //GetDamage(0,0,0);
        BulletChanges(maxAmmunition);
    }

    private void InstantiateStats()
    {
        // Initialize player stats
        maxMoveSpeed = 2.5f;
        moveSpeed = maxMoveSpeed;
        maxlife = 100f + GlobalVariables.healthUpgrade - 1;
        life = maxlife;
        maxAmmunition = 2;
        ammunition = maxAmmunition;
        fireForce = 30f;
        attackSpeed = 1f;
        dornen = 0f;
        baseDamage = 1f + GlobalVariables.damageUpgrade - 1;
        damage = 1f;
        dashmaxCooldown = 5f;
    }

    public void BulletChanges(int ammo)
    {
        foreach (var item in bulletUI)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < ammo; i++)
        {
            bulletUI[i].SetActive(true);
        }
    }

    public void GetDamage(float enemydamage,float length,float dot)
    {

        life -= enemydamage;
        if (life < 0)
            life = 0;       
        
        healthbarUI.SetHealth(life,maxlife);
        lifeText.text = life.ToString() + "/" + maxlife.ToString();

    }
    public void GetHealth(float health)
    {
        if (life < maxlife && life + health <= maxlife)
            life += health;
        else
            if (life + health > maxlife)
                life = maxlife;
        healthbarUI.SetHealth(life, maxlife);
        lifeText.text = life.ToString() + "/" + maxlife.ToString();
    }


    private void InstantiateLists()
    {

        commonItems = new List<string>
        {
            "Piercing 1",
            "Pistol",
            "Helm",
            "Mamas Latschen",
            "Dornen",
            "Fussball",
            "Milch",
            "quick mag",
            "Kleines Waffenwissen",
            "wenig Munni",
            "Dieb"
        };

        rareItems = new List<string>
        {
            "Piercing 2",
            "Ruestungsschuhe",
            "Kaktus an die Ruestung geklebt",
            "Schulausbildung(Amerika)",
            "Gewichte",
            "Marcels Faulheit",
            "Waffenwissen"
        };

        epicItems = new List<string>
        {
            "Piercing 3",
            "Maschine Pistole",
            "Marksmanrifle",
            "Glass Cannon",
            "Ruestung",
            "Laufschuhe",
            "Robins T-Shirt",
            "Robins Melder",
            "Robins IQ",
            "Cedrics Fettruestung"
        };

        legendaryItems = new List<string>
        {
            "Alle Waffen im Besitz",
            "Minigun",
            "50.Cal",
            "Kartenbetrug"
        };

    }

    public void Upgrades(int numberInList, int rarity)
    {


        // Map item names to actions using a dictionary
        Dictionary<string, Action> upgradeActions = rarity switch
        {
            0 => new Dictionary<string, Action>
                {
                    { "Pistol", () => {maxAmmunition += 1; weapon.ChangeBullets(1);}},
                    { "Helm", () => {maxlife += 5f; GetHealth(5); } },
                    { "Mamas Latschen", () => moveSpeed += 0.5f },
                    { "Dornen", () => dornen = 1f },
                    { "Milch", () => damage += 1f },
                    { "quick mag", () => attackSpeed *= 0.9f },
                    { "Kleines Waffenwissen", () => { attackSpeed *= 0.95f; damage += 0.5f; } },
                    { "Dieb", () => damage += 1f },
                    { "Fussball", () => moveSpeed += 0.5f},
                    { "wenig Munni", () => { maxAmmunition += 1; ammunition += 1; weapon.ChangeBullets(ammunition);}}

                },
            1 => new Dictionary<string, Action>
                {
                    { "Ruestungsschuhe", () => {maxlife += 5; GetHealth(5); moveSpeed -= 0.5f; } },
                    { "Waffenwissen", () => {attackSpeed *= 0.8f; damage += 1.5f; maxlife -= 3; moveSpeed -= 0.5f;} },// need fixing movespeed
                    { "Schulausbildung(Amerika)", () => {attackSpeed *= 0.8f; } },
                    { "Gewichte", () => {} },
                    { "Marcels Faulheit", () => {} }
                },
            2 => new Dictionary<string, Action>
                {
                    { "Maschine Pistole", () => {/* Add implementation for epic items */} },
                    { "Marksmanrifle", () => {/* Add implementation for epic items */} },
                    { "Glass Cannon", () => {/* Add implementation for epic items */} },
                    { "Cedrics Fettruestung", () => {/* Add implementation for epic items */} }
                },
            3 => new Dictionary<string, Action>
                {
                    { "Minigun", () => {/* Add implementation for legendary items */} },
                    { "50.Cal", () => {/* Add implementation for legendary items */} },
                    { "Kartenbetrug", () => {/* Add implementation for legendary items */} }
                },
            _ => throw new System.NotImplementedException()
        };

        // Apply upgrades based on rarity and item
        string itemName = rarity switch
        {
            0 => commonItems[numberInList],
            1 => rareItems[numberInList],
            2 => epicItems[numberInList],
            3 => legendaryItems[numberInList],
            _ => throw new System.NotImplementedException()
        };
        if (upgradeActions.ContainsKey(itemName))
        {
            upgradeActions[itemName]();
        }
    }
    private delegate void Action();

    public void DashUI(float dashcd)
    {
        healthbarUI.SetDash(dashcd, dashmaxCooldown);
    }
}
