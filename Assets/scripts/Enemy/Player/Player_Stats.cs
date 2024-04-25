using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class player_Stats : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    private HealthbarUI healthbarUI;
    private TextMeshProUGUI lifeText;
    [SerializeField]
    private GameObject[] bulletUI;

    private PlayerBehaviour playerScript;


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

    private List<string> commonItems;
    private List<string> rareItems;
    private List<string> epicItems;
    private List<string> legendaryItems;


    private void Awake()
    {
        InstantiateStats();
        InstantiateLists();
        InstantiateUi();
        playerScript = GetComponent<PlayerBehaviour>();
    }

    private void InstantiateUi()
    {
        healthbarUI = GameObject.FindGameObjectWithTag("Healthbar").GetComponentInChildren<HealthbarUI>();

        lifeText = GameObject.Find("LifeText").GetComponentInChildren<TextMeshProUGUI>();
        ChangeHealthbar();
        healthbarUI.SetHealth(life, maxlife);

        bulletUI = GameObject.FindGameObjectsWithTag("BulletUI");

        foreach (var item in bulletUI)
        {
            item.SetActive(false);
        }
        BulletChanges(maxAmmunition);
    }

    private void InstantiateStats()
    {
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
        damage = 2f;
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

    public IEnumerator GetDamage(float enemydamage,float length,float dot)
    {
        life -= enemydamage;
        if (life < 0)
            life = 0;       
        
        while(length > 0)
        {
            life -= dot;
            ChangeHealthbar();
            playerScript.CameraShake();
            CheckIfDeath();
            length --;
            yield return new WaitForSeconds(1);
        }
        ChangeHealthbar();
        yield return null;
    }


    private void CheckIfDeath()
    {
        if (life <= 0)
           playerScript.Death();
    }
    public void GetHealth(float health)
    {
        if (life < maxlife && life + health <= maxlife)
            life += health;
        else
            if (life + health > maxlife)
                life = maxlife;
        ChangeHealthbar();
    }

    private void ChangeHealthbar()
    {
        healthbarUI.SetHealth(life, maxlife);
        lifeText.text = life.ToString() + "/" + maxlife.ToString();
    }

    private void InstantiateLists()
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

    }

    public void Upgrades(int numberInList, int rarity)
    {


    
        Dictionary<string, Action> upgradeActions = rarity switch
        {
            0 => new Dictionary<string, Action>
                {
                    { "Hartes Geschoss", () => damage += 0.2f },
                    { "Helm", () => {maxlife += 5f; GetHealth(5); } },
                    { "Laufschuhe", () => moveSpeed += 0.3f },
                    { "Super Dash", () => dashmaxCooldown -= 1f },
                    { "Energy Drink", () => {moveSpeed += 0.2f; dashmaxCooldown -= 0.5f; } },
                    { "Schneller Schuss", () => {attackSpeed *= 0.9f; fireForce += 10f; } },
                    { "Kleines Waffenwissen", () => { attackSpeed *= 0.95f; damage += 0.5f; } },
                    { "Big Shot", () => damage += 0.3f },
                    { "Grosses Magazin", () => {maxAmmunition += 1; BulletChanges(maxAmmunition); } },
                    { "Licht und Schatten", () => {{attackSpeed -= 0.5f; maxAmmunition -= 1; BulletChanges(maxAmmunition);  } } }

                },
            1 => new Dictionary<string, Action>
                {
                    { "Ruestungsschuhe", () => {maxlife += 5; GetHealth(5); moveSpeed -= 0.5f; } },
                    { "Waffenwissen", () => {attackSpeed *= 0.8f; damage += 0.5f; maxlife -= 3; moveSpeed -= 0.5f;} },// need fixing movespeed
                    { "Schulausbildung(Amerika)", () => {attackSpeed *= 0.8f; } },
                    { "Gewichte", () => {moveSpeed -= 0.4f; damage += 2f;} },
                    { "Flower of Speed", () => {moveSpeed += 0.5f;} }
                },
            2 => new Dictionary<string, Action>
                {
                    { "Maschine Pistole", () => {attackSpeed *= 1f;} },
                    { "Marksmanrifle", () => {damage += 1f; attackSpeed *= 0.5f;} },
                    { "Bumm Bumm Frucht", () => {maxlife -= 50f; damage += 2.5f; GetHealth(0);} },
                    { "Schwere Ruestung", () => {maxlife += 50f; GetHealth(50);} }
                },
            3 => new Dictionary<string, Action>
                {
                    { "Minigun", () => {damage /= 2f; attackSpeed -= 3f;} },
                    { "Deathsentence", () => {damage += 5f;} },
                    { "Ultra Boots", () => {moveSpeed += 1.5f;} },
                    { "Legendary Dash", () => {dashmaxCooldown -= 4f;} },
                    { "Glass Cannon", () => {maxlife = 1f; damage += 8f; GetHealth(0);} }
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
