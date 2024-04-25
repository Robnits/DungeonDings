using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wuestengegner : EnemysHauptklasse
{
    private Rigidbody2D rb;
    

    void Start()
    {
        dealDamageToPlayer = GetComponent<DealDamageToPlayer>();
        //dealDamageToPlayer.dmg = 7;
        speed = 2.4f;
        maxlife = 3;
        life = maxlife;
        value = 3;
        droprate = 5;
        healthscript.GetMaxhealth(life);
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player.transform.position);
            transform.Rotate(new Vector3(0, -90, 90), Space.Self);//correcting the original rotation

            if (Vector3.Distance(transform.position, player.transform.position) > 0f)
            {
                Vector2 moveDirection = (player.transform.position - transform.position).normalized;
                rb.velocity = moveDirection * speed;
            }
        }
    }
}
