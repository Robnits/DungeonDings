using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behjaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public Weapon weapon;
    private GameObject scenenwechsel;
    public Animator anim;
    
    private float damage;
    private float movespeed = 2.5f;
    private float life;
    

    Vector2 moveDirection;
    Vector2 mousePosition;

    [SerializeField]
    private UpgradeSO upgradeSO;


    

    private void Awake()
    {
        scenenwechsel = GameObject.FindGameObjectWithTag("Respawn");
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector3(4, 0, 0); 
        life = 10f + upgradeSO.Life;
        damage = upgradeSO.Damage + weapon.GetDamage();
    }

    public float GetDamage()
    {
        return(damage);
    }


    // Start is called before the first frame update
    void Update()
    {
        

        float move_x = Input.GetAxisRaw("Horizontal");
        float move_y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.FireCounter();
        }
        if (move_x != 0 || move_y != 0)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);
       



        moveDirection = new Vector2(move_x, move_y).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (life <= 0)
            Death();

    }
    //Todes animierung und ende des spieles
    private void Death()
    {
        scenenwechsel.GetComponent<scenemanager>().mainmenu();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * movespeed, moveDirection.y * movespeed);

        Vector2 aimdirection = mousePosition - rb.position;
        float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimangle;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            life -= collision.gameObject.GetComponent<Enemy_behaviour>().GetDamage();
        if (collision.gameObject.CompareTag("Rats"))
            life -= collision.gameObject.GetComponent<Rats>().GetDamage();
    }
        private bool instantiateList = false; 
        private List<string> commonItems = new List<string>();
        private List<string> rareItems = new List<string>();
        private List<string> EpicItems = new List<string>();
        private List<string> LegendaryItems = new List<string>();
    public void Upgrades(int NumberInList)
    {
        if (!instantiateList)
        {
            instantiateList = true;
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

        print(commonItems[NumberInList]);
        switch (commonItems[NumberInList])
        {
            case "Minigun":
                
                break;
            case "Maschine Pistole":
                break;
            case "50.Cal":
                break;
            case "Marksmanrifle":
                break;
            case "Piercing 1":
                break;
            case "Piercing 2":
                break;
            case "Glass Cannon":
                break;
            case "Mamas Latschen":
                break;
            case "Dornen":
                break;
            case "Milch":
                break;
            case "Gewichte":
                break;
        }
    }
}
 