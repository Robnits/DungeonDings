using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Devil : EnemysHauptklasse
{
   
    public GameObject weapon;

    public bool playerIsInRange;
    public bool playerIsInMelee;

    private float Countdown = 3f;
    private Rigidbody2D rb;
    private bool isshooting;
    // Start is called before the first frame update
    void Start()
    {
        life = 10f;
        damage = 3f;
        value = 10f;
        speed = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

        Vector3 directionToPlayer = player.transform.position - transform.position;

        // Calculate the angle to rotate towards the player
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg + 90;

        // Rotate the enemy to face the player
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if (distance <= 1)
        {
            StartCoroutine(MoveBackwards());
            playerIsInMelee = true;
            playerIsInRange = false;
            StartCoroutine(MeleeAttack());
        }
        else
        {
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            rb.velocity = moveDirection * speed;

            playerIsInMelee = false;
            playerIsInRange = true;
            //StartCoroutine(FireballCountdown());
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerIsInMelee == false)
        {
            playerIsInRange = true;
            StartCoroutine(FireballCountdown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }
    IEnumerator MoveBackwards()
    {
        float timePassed = 0;
        while (timePassed < 3)
        { 
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            rb.velocity = moveDirection * speed * -1;
            timePassed += Time.deltaTime;

            yield return null;
        }

    }
    IEnumerator FireballCountdown()
    {
        if (playerIsInRange && !isshooting)
        {
            isshooting = true;
            weapon.GetComponent<DevilFireBall>().fireFireball();
            yield return new WaitForSeconds(Countdown);
            isshooting=false;
            StartCoroutine(FireballCountdown());
        }
    }
    IEnumerator MeleeAttack()
    {
        yield return null;
    }
}
