using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behjaviour : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Weapon weapon;
    public GameObject scenenwechsel;
    public Animator anim;
    
    private float damage;
    private float movespeed = 5f;
    private float life;
    

    Vector2 moveDirection;
    Vector2 mousePosition;

    [SerializeField]
    private UpgradeSO upgradeSO;

    

    private void Awake()
    {
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
            weapon.fire();
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
}
 