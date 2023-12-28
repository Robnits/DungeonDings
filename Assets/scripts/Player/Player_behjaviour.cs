using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behjaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public Weapon weapon;
    private GameObject scenenwechsel;
    public Animator anim;
    Player_Stats stats;
    
    Vector2 moveDirection;
    Vector2 mousePosition;

    private bool invincibleAfterDmg = false;

    [SerializeField]
    private UpgradeSO upgradeSO;

    private void Awake()
    {
        stats = gameObject.GetComponent<Player_Stats>();
        scenenwechsel = GameObject.FindGameObjectWithTag("Respawn");
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector3(0, 0, 0); 
    }

    public float GetDamage()
    {
        return (stats.damage * stats.baseDamage);
    }

    // Start is called before the first frame update
    void Update()
    {
        

        float move_x = Input.GetAxisRaw("Horizontal");
        float move_y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
            weapon.FireCounter();
        
        if (move_x != 0 || move_y != 0)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);
       
        moveDirection = new Vector2(move_x, move_y).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (stats.life <= 0)
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
        rb.velocity = new Vector2(moveDirection.x * stats.moveSpeed, moveDirection.y * stats.moveSpeed);

        Vector2 aimdirection = mousePosition - rb.position;
        float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimangle;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && !invincibleAfterDmg)
        {
            if (collision.gameObject.GetComponentInChildren<Rats>() == true)
            {
                StartCoroutine(InvincibleTime(collision));
            }
        }
    }

    IEnumerator InvincibleTime(Collision2D collision)
    {
        invincibleAfterDmg = true;
        stats.GetDamage(collision.gameObject.GetComponent<Rats>().GetDamage());
        yield return new WaitForSeconds(0.5f);
        invincibleAfterDmg = false;
    }
}
 