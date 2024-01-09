using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rats : EnemysHauptklasse
{

    private Rigidbody2D rb;
    private Animator anim;
    private void Start()
    {
        speed = 1.0f;
        life = 3f;
        value = 1f;
        damage = 1f;
        droprate = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (player == null)
        {
            Death();
        }
        else
        {
            if (rb.velocity.x != 0 || rb.velocity.y != 0)
                anim.SetBool("IsMoving", true);
            else
                anim.SetBool("IsMoving", false);
            transform.LookAt(player.transform.position);
            transform.Rotate(new Vector3(0, -90, 90), Space.Self);//correcting the original rotation

            // Move towards the player
            if (Vector3.Distance(transform.position, player.transform.position) > 0f)
            {
                Vector2 moveDirection = (player.transform.position - transform.position).normalized;
                rb.velocity = moveDirection * speed;
            }
            else
            {
                rb.velocity = Vector2.zero; // Stop moving if close to the player.
            }
        }
    }
}
