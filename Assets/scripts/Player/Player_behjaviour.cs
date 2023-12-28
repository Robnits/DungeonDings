using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behjaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    Weapon weapon;

    private GameObject scenenwechsel;

    [SerializeField]
    private Animator anim;
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
        Movement();
        if (Input.GetMouseButtonDown(0))
            weapon.FireCounter();

        if (Input.GetKeyDown(KeyCode.Space))
            Dash();

        if (stats.life <= 0)
            Death();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);

        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
                if (!invincibleAfterDmg)
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

    private void Dash()
    {
        rb.velocity += moveDirection * stats.moveSpeed;
    }
}
 