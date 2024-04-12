using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour
{
    public DevilFireBall weapon;

    public bool playerIsInRange;
    public bool playerIsInMelee;

    private readonly float Countdown = 3f;
    private Rigidbody2D rb;
    private bool isshooting;
    private int fireballcounter;
    // Start is called before the first frame update
    void Start()
    {
        life = 15f;
        damage = 25f;
        value = 10f;
        speed = 0.3f;
        droprate = 90;
        healthscript.GetMaxhealth(life);
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {

            if (playerIsInRange)
            {
                float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

                Vector3 directionToPlayer = player.transform.position - transform.position;

                // Calculate the angle to rotate towards the player
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg + 90;

                // Rotate the enemy to face the player
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                if (distance <= 3)
                {
                    StartCoroutine(MoveBackwards());
                }
                else
                {
                    Vector2 moveDirection = (player.transform.position - transform.position).normalized;
                    rb.velocity = moveDirection * speed;
                }
            }
        }
    }
    private new void OnTriggerEnter2D(Collider2D collision)
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
        while (timePassed < 2 && player != null)
        { 
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            rb.velocity =  speed * -1 * moveDirection;
            timePassed += Time.deltaTime;

            yield return null;
        }

    }
    IEnumerator FireballCountdown()
    {
        
        if (playerIsInRange && !isshooting && fireballcounter < 3)
        {
            fireballcounter++;
            isshooting = true;
            weapon.GetComponent<DevilFireBall>().FireFireball();
            yield return new WaitForSeconds(Countdown);
            isshooting=false;
            StartCoroutine(FireballCountdown());
        }
        else if(playerIsInRange && !isshooting && fireballcounter >= 3)
        {
            isshooting = true;
            yield  return new WaitForSeconds(0.3f);
            weapon.GetComponent<DevilFireBall>().RiesenFeuerballJunge();
            yield return new WaitForSeconds(0.3f);
            fireballcounter = -1;
            isshooting = false;
            StartCoroutine(FireballCountdown());
            
        }
    }
}
